// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
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
    case DatabaseProvider.SQLServerContainer:
        var sqlServerDbContainer = builder.AddSqlServer(Constants.Infrastructure.SqlServerContainerName).AddDatabase(ApplicationDbContext.DatabaseName);
        webApp.WithReference(sqlServerDbContainer);
        break;

    case DatabaseProvider.PostgresSQLContainer:
        var postgresDbContainer = builder.AddPostgres(Constants.Infrastructure.PostgressContainerName).AddDatabase(ApplicationDbContext.DatabaseName);
        webApp.WithReference(postgresDbContainer);
        break;

    default:
        // Connect to external non-aspire DB.
        break;
}

webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_Accounts>("AdminAssistant.Services.Accounts"));
webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>("AdminAssistant.Services.AssetRegister"));
webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_Core>("AdminAssistant.Services.Core"));
builder.Build().Run();
