// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
using AdminAssistant.Shared;
using Ardalis.GuardClauses;
using Aspire.Hosting;
using Microsoft.Extensions.Configuration;
using SimonGeering.Framework.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Config Settings ...
var configurationSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
Guard.Against.Null(configurationSettings);

if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
    throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>(Constants.Services.AccountsApi)
    .WithHealthCheck();
var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>(Constants.Services.AdminApi)
    .WithHealthCheck();
var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>(Constants.Services.AssetRegisterApi)
    .WithHealthCheck();
var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>(Constants.Services.BudgetApi)
    .WithHealthCheck();
var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>(Constants.Services.CalendarApi)
    .WithHealthCheck();
var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>(Constants.Services.ContactsApi)
    .WithHealthCheck();
var core = builder.AddProject<Projects.AdminAssistant_Services_Core>(Constants.Services.CoreApi)
    .WithHealthCheck();
var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>(Constants.Services.DocumentsApi)
    .WithHealthCheck();
var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>(Constants.Services.MailApi)
    .WithHealthCheck();
var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>(Constants.Services.NotesApi)
    .WithHealthCheck();
var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>(Constants.Services.ScheduledPaymentsApi)
    .WithHealthCheck();
var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>(Constants.Services.TasksApi)
    .WithHealthCheck();

// EF Database management ...
var databaseMigrationWorkerService = builder.AddProject<Projects.AdminAssistant_Aspire_DatabaseMigrationWorkerService>(Constants.Services.DatabaseMigrationWorkerService);

// Database Server ...
switch (databaseProvider)
{
    case DatabaseProvider.SQLServer:
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        throw new NotImplementedException("TODO - DatabaseProvider.SQLServer");
    // var sqlServerExternalDb = builder.AddSqlServerConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
    // webApp.WithReference(sqlServerExternalDb);
    // break;

    case DatabaseProvider.SQLServerLocalDB:

        //var sqlServerLocalDb = builder.AddConnectionString()
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        throw new NotImplementedException("TODO - DatabaseProvider.SQLServerLocalDB");
        //var moo = builder.AddConnectionString
    // var sqlServerLocalDb = builder.AddSqlServerConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
    // webApp.WithReference(sqlServerLocalDb);
    // break;

    case DatabaseProvider.SQLite:
        throw new System.NotSupportedException("SQLite database provider not currently supported.");

    case DatabaseProvider.PostgresSQL:
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        throw new NotImplementedException("TODO - DatabaseProvider.PostgresSQL");
    // var postgresExternalDb = builder.AddPostgresConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
    // webApp.WithReference(postgresExternalDb);
    // break;

    case DatabaseProvider.SQLServerContainer:
        var sqlServerDbContainer = builder
            .AddSqlServer(configurationSettings.AspireSqlServerName)
            .WithHealthCheck()
            .AddDatabase(configurationSettings.AspireDatabaseName);

        accounts.WithReference(sqlServerDbContainer);
        admin.WithReference(sqlServerDbContainer);
        assetRegister.WithReference(sqlServerDbContainer);
        budget.WithReference(sqlServerDbContainer);
        calendar.WithReference(sqlServerDbContainer);
        contacts.WithReference(sqlServerDbContainer);
        core.WithReference(sqlServerDbContainer);
        documents.WithReference(sqlServerDbContainer);
        mail.WithReference(sqlServerDbContainer);
        notes.WithReference(sqlServerDbContainer);
        scheduledPayments.WithReference(sqlServerDbContainer);
        tasks.WithReference(sqlServerDbContainer);

        databaseMigrationWorkerService.WithReference(sqlServerDbContainer)
            .WaitFor(sqlServerDbContainer);
        break;

    case DatabaseProvider.PostgresSQLContainer:
        var postgresDbContainer = builder
            .AddPostgres(configurationSettings.AspirePostgresServerName)
            .WithHealthCheck()
            .WithPgAdmin()
            .AddDatabase(configurationSettings.AspireDatabaseName);

        accounts.WithReference(postgresDbContainer);
        admin.WithReference(postgresDbContainer);
        assetRegister.WithReference(postgresDbContainer);
        budget.WithReference(postgresDbContainer);
        calendar.WithReference(postgresDbContainer);
        contacts.WithReference(postgresDbContainer);
        core.WithReference(postgresDbContainer);
        documents.WithReference(postgresDbContainer);
        mail.WithReference(postgresDbContainer);
        notes.WithReference(postgresDbContainer);
        scheduledPayments.WithReference(postgresDbContainer);
        tasks.WithReference(postgresDbContainer);

        databaseMigrationWorkerService.WithReference(postgresDbContainer)
            .WaitFor(postgresDbContainer);
        break;
}

// Gateway ...
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
    .WithHealthCheck();

// Background Job Host ...
builder.AddProject<Projects.AdminAssistant_Hangfire>(Constants.Services.BackgroundJobHost)
    .WithReference(gateway)
    .WithHealthCheck()
    .WaitFor(databaseMigrationWorkerService)
    .WaitFor(gateway);

// Main App ...
builder.AddProject<Projects.AdminAssistant_Blazor_Server>(configurationSettings.AspireServerAppName)
    .WithReference(gateway)
    .WithHealthCheck()
    .WaitFor(databaseMigrationWorkerService)
    .WaitFor(gateway);

// Retro console UI ... :-)
builder.AddProject<Projects.AdminAssistant_Retro>(Constants.Services.RetroConsole)
    .WithReference(gateway)
    .WaitFor(databaseMigrationWorkerService)
    .WaitFor(gateway)
    .ExcludeFromManifest();

builder.Build().Run();
#pragma warning restore S1481
