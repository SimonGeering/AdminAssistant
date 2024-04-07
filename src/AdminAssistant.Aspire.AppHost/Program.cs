// ReSharper disable UnusedVariable
#pragma warning disable S1481

// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
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

// Main App ...
var webApp = builder.AddProject<Projects.AdminAssistant_Blazor_Server>(configurationSettings.AspireServerAppName);

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
        webApp.WithReference(sqlServerDbContainer);
        break;

    case DatabaseProvider.PostgresSQLContainer:
        var postgresDbContainer = builder
            .AddPostgres(configurationSettings.AspirePostgresServerName)
            .WithPgAdmin()
            .AddDatabase(configurationSettings.AspireDatabaseName);
        webApp.WithReference(postgresDbContainer);
        break;
}

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>("AdminAssistant-AccountsApi");
var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>("AdminAssistant-AdminApi");
var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>("AdminAssistant-AssetRegisterApi");
var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>("AdminAssistant-BudgetApi");
var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>("AdminAssistant-CalendarApi");
var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>("AdminAssistant-ContactsApi");
var core = builder.AddProject<Projects.AdminAssistant_Services_Core>("AdminAssistant-CoreApi");
var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>("AdminAssistant-DocumentsApi");
var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>("AdminAssistant-MailApi");
var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>("AdminAssistant-NotesApi");
var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>("AdminAssistant-ScheduledPaymentsApi");
var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>("AdminAssistant-TasksApi");

// Background Job Host ...
var backgroundJobHost = builder.AddProject<Projects.AdminAssistant_Hangfire>("AdminAssistant-HangFire");

// Gateway ...
var gateway = builder.AddProject<Projects.AdminAssistant_Gateway>("AdminAssistant-Gateway");

webApp.WithReference(gateway);

builder.Build().Run();
#pragma warning restore S1481
