// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("admin-assistant");

// IAM Server ...
var keycloak = builder.AddKeycloak(Constants.IAMServerName, 8080)
    .WithDataVolume("keycloak-data")
    .WithLifetime(ContainerLifetime.Persistent);

// Database Server ...
var postgres = builder.AddPostgres(Constants.DatabaseServerName)
    .WithDataVolume("postgresql-data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var pgAdmin = postgres.WithPgAdmin(containerName: Constants.DatabaseServerAdminDashboardName)
    .WithEnvironment("PGADMIN_CONFIG_THEME", "dark")
    .WithLifetime(ContainerLifetime.Persistent);

var applicationDatabase = postgres.AddDatabase(Constants.ApplicationDatabaseName)
    .WithParentRelationship(postgres);

var scheduledJobDatabase = postgres.AddDatabase(Constants.ScheduledJobDatabaseName)
    .WithParentRelationship(postgres);

// Data Cache ...
var cache = builder.AddRedis(Constants.CacheName)
    .WithDataVolume("redis-data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var redisInsight = cache.WithRedisInsight(containerName: Constants.CacheAdminDashboardName)
    .WithLifetime(ContainerLifetime.Persistent);

// Message Buss ...
#pragma warning disable S125
// Example Parameters ...
// "Parameters": {
//     "msgBusAdminUserName": "secretusername",
//     "msgBusAdminPassword": "secretpassword"
// }
#pragma warning restore S125

var msgBusAdminUsername = builder.AddParameter("msgBusAdminUsername", secret: true);
var msgBusAdminPassword = builder.AddParameter("msgBusAdminPassword", secret: true);

var msgBus = builder.AddRabbitMQ(Constants.MessageBusName, msgBusAdminUsername, msgBusAdminPassword)
    .WithDataVolume("rabbitmq-data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var msgBusAdmin = msgBus.WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

#pragma warning disable S125
/*
      // var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.DatabaseMigrationWorkerService)
   //     .WithReference(applicationDatabase)
   //     .WaitFor(postgres)
   //     .WithParentRelationship(postgres);
*/
#pragma warning restore S125

// Web API ...
var api = builder.AddProject<Projects.AdminAssistant_Api>(Constants.Api)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(applicationDatabase).WaitFor(applicationDatabase)
    .WithReference(msgBus).WaitFor(msgBus);
    //.WithAnnotation("Swagger UI", "swagger")
    //.WithAnnotation("OpenAPI JSON", "openapi/v1.json");

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
    .ExcludeFromManifest(); // Not a web app

#pragma warning disable S125
// Retro console UI ... :-)
//builder.AddProject<Projects.AdminAssistant_Retro>(Constants.RetroConsole)
    //.WaitFor(databaseMigrationWorkerService)
    //.ExcludeFromManifest(); // Not a web app
#pragma warning restore S125

await builder.Build().RunAsync();


