// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;

var builder = DistributedApplication.CreateBuilder(args);

// Database Server ...
var sqlServer = builder
    .AddSqlServer(Constants.DatabaseServerName);

var applicationDatabase = sqlServer.AddDatabase(Constants.ApplicationDatabaseName);
var scheduledJobDatabase = sqlServer.AddDatabase(Constants.ScheduledJobDatabase);

var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.DatabaseMigrationWorkerService)
    .WithReference(applicationDatabase)
    .WaitFor(sqlServer);

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>(Constants.Services.AccountsApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>(Constants.Services.AdminApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>(Constants.Services.AssetRegisterApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>(Constants.Services.BudgetApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>(Constants.Services.CalendarApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>(Constants.Services.ContactsApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var core = builder.AddProject<Projects.AdminAssistant_Services_Core>(Constants.Services.CoreApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>(Constants.Services.DocumentsApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>(Constants.Services.MailApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>(Constants.Services.NotesApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>(Constants.Services.ScheduledPaymentsApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>(Constants.Services.TasksApi)
    .WithReference(applicationDatabase)
    .WaitFor(databaseMigrationWorkerService);

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
