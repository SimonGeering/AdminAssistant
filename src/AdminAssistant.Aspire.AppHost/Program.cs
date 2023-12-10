using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Shared;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using SimonGeering.Framework.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var configurationSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
Guard.Against.Null(configurationSettings);

if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
    throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

var webApp = builder.AddProject<Projects.AdminAssistant_Blazor_Server>("AdminAssistant.Blazor.Server");

switch (databaseProvider)
{
    case DatabaseProvider.SQLServer:
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        var sqlServerExternalDb = builder.AddSqlServerConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
        webApp.WithReference(sqlServerExternalDb);
        break;

    case DatabaseProvider.SQLServerLocalDB:
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        var sqlServerLocalDb = builder.AddSqlServerConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
        webApp.WithReference(sqlServerLocalDb);
        break;

    case DatabaseProvider.SQLite:
        // Unsupported.
        break;

    case DatabaseProvider.PostgresSQL:
        Guard.Against.NullOrEmpty(configurationSettings.ConnectionString);
        var postgresExternalDb = builder.AddPostgresConnection(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName, configurationSettings.ConnectionString);
        webApp.WithReference(postgresExternalDb);
        break;

    case DatabaseProvider.SQLServerContainer:
        var sqlServerDbContainer = builder.AddSqlServerContainer("AdminAssistant_SQLServer").AddDatabase(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName);
        webApp.WithReference(sqlServerDbContainer);
        break;

    case DatabaseProvider.PostgresSQLContainer:
        var postgresDbContainer = builder.AddPostgresContainer("AdminAssistant_Postgres").AddDatabase(ConfigurationSettings.AdminAssistantWellKnownConnectionStringName);
        webApp.WithReference(postgresDbContainer);
        break;
}

builder.Build().Run();
