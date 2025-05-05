// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using System.Reflection.Metadata;
using AdminAssistant;

var builder = DistributedApplication.CreateBuilder(args);

// IAM Server ...
var keycloak = builder.AddKeycloak(Constants.IAMServerName, 8080)
    .WithDataVolume("keycloak-data")
    .WithLifetime(ContainerLifetime.Persistent);

// Database Server ...
var postgres = builder.AddPostgres(Constants.DatabaseServerName)
    .WithDataVolume("postgresql-data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var pgAdmin = postgres.WithPgAdmin()
    .WithEnvironment("PGADMIN_CONFIG_THEME", "dark")
    .WithLifetime(ContainerLifetime.Persistent);

var applicationDatabase = postgres.AddDatabase(Constants.ApplicationDatabaseName)
    .WithParentRelationship(postgres);

var scheduledJobDatabase = postgres.AddDatabase(Constants.ScheduledJobDatabaseName)
    .WithParentRelationship(postgres);

var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.DatabaseMigrationWorkerService)
    .WithReference(applicationDatabase)
    .WaitFor(postgres)
    .WithParentRelationship(postgres);

// Data Cache ...
var cache = builder.AddRedis(Constants.CacheName)
    .WithDataVolume("redis-data", isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var redisInsight = cache.WithRedisInsight()
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

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>(Constants.Services.AccountsApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>(Constants.Services.AdminApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>(Constants.Services.AssetRegisterApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>(Constants.Services.BudgetApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>(Constants.Services.CalendarApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>(Constants.Services.ContactsApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var core = builder.AddProject<Projects.AdminAssistant_Services_Core>(Constants.Services.CoreApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>(Constants.Services.DocumentsApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>(Constants.Services.MailApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>(Constants.Services.NotesApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>(Constants.Services.ScheduledPaymentsApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>(Constants.Services.TasksApi)
    .WithReference(applicationDatabase).WaitFor(databaseMigrationWorkerService)
    .WithReference(msgBus).WaitFor(msgBus)
    .WithReference(cache).WaitFor(cache);

// API Gateway ...
var gateway = builder.AddProject<Projects.AdminAssistant_Gateway>(Constants.Services.Gateway)
    .WithReference(accounts).WaitFor(accounts)
    .WithReference(admin).WaitFor(admin)
    .WithReference(assetRegister).WaitFor(assetRegister)
    .WithReference(budget).WaitFor(budget)
    .WithReference(calendar).WaitFor(calendar)
    .WithReference(contacts).WaitFor(contacts)
    .WithReference(core).WaitFor(core)
    .WithReference(documents).WaitFor(documents)
    .WithReference(mail).WaitFor(mail)
    .WithReference(notes).WaitFor(notes)
    .WithReference(scheduledPayments).WaitFor(scheduledPayments)
    .WithReference(tasks).WaitFor(tasks)
    .WaitFor(databaseMigrationWorkerService);

gateway.WithReference(gateway).WithExternalHttpEndpoints();

accounts.WithParentRelationship(gateway);
admin.WithParentRelationship(gateway);
assetRegister.WithParentRelationship(gateway);
budget.WithParentRelationship(gateway);
calendar.WithParentRelationship(gateway);
contacts.WithParentRelationship(gateway);
core.WithParentRelationship(gateway);
documents.WithParentRelationship(gateway);
mail.WithParentRelationship(gateway);
notes.WithParentRelationship(gateway);
scheduledPayments.WithParentRelationship(gateway);
tasks.WithParentRelationship(gateway);

// Scheduled Job Host ...
builder.AddProject<Projects.AdminAssistant_Hangfire>(Constants.Services.ScheduledJobHost)
    .WithReference(gateway)
    .WithReference(scheduledJobDatabase)
    .WaitFor(databaseMigrationWorkerService); // Should be waiting for gateway in the long run

// Main App ...
builder.AddProject<Projects.AdminAssistant_ServerSideBlazor>(Constants.ServerAppName)
    .WithReference(gateway);

// Avalonia App ...
builder.AddProject<Projects.AdminAssistant_Avalonia>(Constants.AvaloniaAppName)
    .WithReference(gateway);

// Retro console UI ... :-)
builder.AddProject<Projects.AdminAssistant_Retro>(Constants.RetroConsole)
    .WithReference(gateway)
    .WaitFor(databaseMigrationWorkerService)
    .ExcludeFromManifest(); // Should be waiting for gateway in the long run

await builder.Build().RunAsync();

#pragma warning restore S1481
