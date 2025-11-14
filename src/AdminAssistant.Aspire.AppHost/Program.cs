// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
using Aspire.Hosting.Docker.Resources.ComposeNodes;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddDockerComposeEnvironment("AdminAssistantEnvironment")
    .WithProperties(env =>
    {
        env.DefaultNetworkName = "AdminAssistant-Network";
    });

// IAM Server ...
var keycloak = builder.AddKeycloak(Constants.IAMServerName, 8080)
    .WithDataVolume($"{Constants.IAMServerName}-Data")
    .WithLifetime(ContainerLifetime.Persistent);

// Database Server ...
var postgres = builder.AddPostgres(Constants.DatabaseServerName)
    .WithDataVolume($"{Constants.DatabaseServerName}-Data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var pgAdmin = postgres.WithPgAdmin(containerName: Constants.DatabaseServerAdminDashboardName)
    .WithEnvironment("PGADMIN_CONFIG_THEME", "dark")
    .WithLifetime(ContainerLifetime.Persistent);

var applicationDatabase = postgres.AddDatabase(Constants.ApplicationDatabaseName)
    .WithParentRelationship(postgres);

var scheduledJobDatabase = postgres.AddDatabase(Constants.ScheduledJobDatabaseName)
    .WithParentRelationship(postgres);

var migrations = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationService>(Constants.DatabaseMigrationWorkerService)
    .WithReference(applicationDatabase)
    .WaitFor(applicationDatabase);

// Data Cache ...
var cache = builder.AddRedis(Constants.CacheName)
    .WithDataVolume($"{Constants.CacheName}-Data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

cache.WithRedisInsight(containerName: Constants.CacheAdminDashboardName)
    .WithLifetime(ContainerLifetime.Persistent);

// Message Buss ...
var msgBusAdminUsername = builder.AddParameter("msgBusAdminUsername", secret: true);
var msgBusAdminPassword = builder.AddParameter("msgBusAdminPassword", secret: true);

var msgBus = builder.AddRabbitMQ(Constants.MessageBusName, msgBusAdminUsername, msgBusAdminPassword)
    .WithDataVolume($"{Constants.MessageBusName}-Data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

msgBus.WithManagementPlugin()
    .WithContainerName(Constants.MessageBusAdminDashboardName)
    .WithLifetime(ContainerLifetime.Persistent);

// Web API ...
var api = builder.AddProject<Projects.AdminAssistant_Api>(Constants.Api)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(migrations).WaitForCompletion(migrations)
    .WithReference(applicationDatabase).WaitFor(applicationDatabase)
    .WithUrl("/api/docs/index.html"); //OpenAPI JSON = /openapi/v1.json

// Main App ...
builder.AddProject<Projects.AdminAssistant_Web>(Constants.WebAppName)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache);

// Avalonia App ...
builder.AddProject<Projects.AdminAssistant_AvaloniaApp>(Constants.AvaloniaAppName)
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache)
    .WithExplicitStart()
    .ExcludeFromManifest(); // Not a web app

// Retro console UI ... :-)
builder.AddProject<Projects.AdminAssistant_Retro>(Constants.RetroConsole)
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache)
    .WithExplicitStart()
    .ExcludeFromManifest(); // Not a web app

await builder.Build().RunAsync();
