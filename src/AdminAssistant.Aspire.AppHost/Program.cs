// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
using AdminAssistant.Shared;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using SimonGeering.Framework.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Config Settings ...
var configurationSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
Guard.Against.Null(configurationSettings);

if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
    throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>(Constants.Services.AccountsApi);
var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>(Constants.Services.AdminApi);
var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>(Constants.Services.AssetRegisterApi);
var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>(Constants.Services.BudgetApi);
var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>(Constants.Services.CalendarApi);
var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>(Constants.Services.ContactsApi);
var core = builder.AddProject<Projects.AdminAssistant_Services_Core>(Constants.Services.CoreApi);
var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>(Constants.Services.DocumentsApi);
var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>(Constants.Services.MailApi);
var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>(Constants.Services.NotesApi);
var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>(Constants.Services.ScheduledPaymentsApi);
var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>(Constants.Services.TasksApi);

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
            .AddDatabase(configurationSettings.AspireDatabaseName);
        //webApp.WithReference(sqlServerDbContainer, configurationSettings.AspireDatabaseName);
        //accounts.WithReference(sqlServerDbContainer)
        break;

    case DatabaseProvider.PostgresSQLContainer:
        var postgresDbContainer = builder
            .AddPostgres(configurationSettings.AspirePostgresServerName)
            .WithPgAdmin()
            .AddDatabase(configurationSettings.AspireDatabaseName);
        //webApp.WithReference(postgresDbContainer, configurationSettings.AspireDatabaseName);
        break;
}


// Background Job Host ...
var backgroundJobHost = builder.AddProject<Projects.AdminAssistant_Hangfire>(Constants.Services.BackgroundJobHost);

// Gateway ...
var gateway = builder.AddProject<Projects.AdminAssistant_Gateway>(Constants.Services.Gateway)
    .WithReference(accounts)
    .WithReference(admin)
    .WithReference(assetRegister)
    .WithReference(budget)
    .WithReference(calendar)
    .WithReference(contacts)
    .WithReference(core)
    .WithReference(documents)
    .WithReference(mail)
    .WithReference(notes)
    .WithReference(scheduledPayments)
    .WithReference(tasks);

// Main App ...
var webApp = builder.AddProject<Projects.AdminAssistant_Blazor_Server>(configurationSettings.AspireServerAppName)
    .WithReference(gateway);

var retroApp = builder.AddProject<Projects.AdminAssistant_Retro>("RetroConsole")
    .WithReference(gateway);

builder.Build().Run();
#pragma warning restore S1481
