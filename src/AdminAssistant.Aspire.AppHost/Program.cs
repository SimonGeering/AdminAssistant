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
/*
   // var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.DatabaseMigrationWorkerService)
   //     .WithReference(applicationDatabase)
   //     .WaitFor(postgres)
   //     .WithParentRelationship(postgres);
*/

// Main App ...
builder.AddProject<Projects.AdminAssistant_Blazor_Server>(Constants.ServerAppName)
    .WithReference(applicationDatabase)
    //.WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache)
    .WithExternalHttpEndpoints();

// Retro console UI ... :-)
builder.AddProject<Projects.AdminAssistant_Retro>(Constants.RetroConsole)
    //.WaitFor(databaseMigrationWorkerService)
    .ExcludeFromManifest(); // Not a web app

await builder.Build().RunAsync();


