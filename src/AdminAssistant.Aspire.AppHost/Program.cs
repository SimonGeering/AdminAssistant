// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;

var builder = DistributedApplication.CreateBuilder(args);

// Database Server ...
var sqlServer = builder
    .AddSqlServer(Constants.DatabaseServerName)
    .WithHealthCheck();

var applicationDatabase = sqlServer.AddDatabase(Constants.ApplicationDatabaseName)
    .WithHealthCheck()
    .WaitFor(sqlServer);

var scheduledJobDatabase = sqlServer.AddDatabase(Constants.ScheduledJobDatabase)
    .WithHealthCheck()
    .WaitFor(sqlServer);

var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.Services.DatabaseMigrationWorkerService)
    .WithReference(applicationDatabase)
    .WaitFor(sqlServer);

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>(Constants.Services.AccountsApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>(Constants.Services.AdminApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>(Constants.Services.AssetRegisterApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>(Constants.Services.BudgetApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>(Constants.Services.CalendarApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();
var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>(Constants.Services.ContactsApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var core = builder.AddProject<Projects.AdminAssistant_Services_Core>(Constants.Services.CoreApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>(Constants.Services.DocumentsApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>(Constants.Services.MailApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>(Constants.Services.NotesApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>(Constants.Services.ScheduledPaymentsApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>(Constants.Services.TasksApi)
    .WithReference(applicationDatabase)
    .WithHealthCheck();

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
    .WithHealthCheck()
    .WaitFor(databaseMigrationWorkerService);

// Scheduled Job Host ...
builder.AddProject<Projects.AdminAssistant_Hangfire>(Constants.Services.ScheduledJobHost)
    .WithReference(gateway)
    .WithReference(scheduledJobDatabase)
    .WithHealthCheck()
    .WaitFor(databaseMigrationWorkerService); // Should be waiting for gateway in the long run

// Main App ...
builder.AddProject<Projects.AdminAssistant_ServerSideBlazor>(Constants.ServerAppName)
    .WithReference(gateway)
    .WithHealthCheck();
    // .WaitFor(databaseMigrationWorkerService); // Should be waiting for gateway in the long run

// Retro console UI ... :-)
builder.AddProject<Projects.AdminAssistant_Retro>(Constants.Services.RetroConsole)
    .WithReference(gateway)
    .WaitFor(databaseMigrationWorkerService)
    .ExcludeFromManifest(); // Should be waiting for gateway in the long run

await builder.Build().RunAsync();

#pragma warning restore S1481
