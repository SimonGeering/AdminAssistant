// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddDockerComposeEnvironment("AdminAssistantEnvironment")
    .WithProperties(env => env.DefaultNetworkName = "AdminAssistant-Network");

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

var migrations = builder.AddDotnetProject(Constants.DatabaseMigrationWorkerService,
        "../AdminAssistant.Aspire.DatabaseMigrationService/AdminAssistant.Aspire.DatabaseMigrationService.csproj")
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
    .WithLifetime(ContainerLifetime.Persistent)
    .WithChildRelationship(msgBusAdminUsername)
    .WithChildRelationship(msgBusAdminPassword);

msgBus.WithManagementPlugin()
    .WithContainerName(Constants.MessageBusAdminDashboardName)
    .WithLifetime(ContainerLifetime.Persistent);

// Web API ...
var api = builder.AddDotnetProject(Constants.Api,"../AdminAssistant.Api/AdminAssistant.Api.csproj")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(migrations).WaitForCompletion(migrations)
    .WithReference(applicationDatabase).WaitFor(applicationDatabase)
    .WithUrl("/api/docs/index.html"); //OpenAPI JSON = /openapi/v1.json

// Main App ...
builder.AddDotnetProject(Constants.WebAppName,"../AdminAssistant.Web/AdminAssistant.Web.csproj")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache);

// Avalonia App ...
builder.AddDotnetProject(Constants.AvaloniaAppName,"../AdminAssistant.AvaloniaApp/AdminAssistant.AvaloniaApp.csproj")
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache)
    .WithExplicitStart()
    .ExcludeFromManifest(); // Not a web app

// Retro console UI ... :-)
builder.AddDotnetProject(Constants.RetroConsole,"../AdminAssistant.Retro/AdminAssistant.Retro.csproj")
    .WithReference(api).WaitFor(api)
    .WithReference(cache).WaitFor(cache)
    .WithExplicitStart()
    .ExcludeFromManifest(); // Not a web app

await builder.Build().RunAsync().ConfigureAwait(false);
