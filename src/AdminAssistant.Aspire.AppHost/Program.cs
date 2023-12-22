// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
//using AdminAssistant.Infrastructure.EntityFramework;
//using AdminAssistant.Shared;
//using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using SimonGeering.Framework.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

//var configurationSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
//Guard.Against.Null(configurationSettings);

//if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
//    throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

//var webApp = builder.AddProject<Projects.AdminAssistant_Blazor_Server>("AdminAssistant.Blazor.Server");
var webApp = builder.AddProject<Projects.AdminAssistant_Blazor>("AdminAssistant.Blazor");

//switch (databaseProvider)
//{    
//    case DatabaseProvider.SQLServerContainer:
//        var sqlServerDbContainer = builder.AddSqlServer(Constants.Infrastructure.SqlServerContainerName).AddDatabase(ApplicationDbContext.DatabaseName);
//        webApp.WithReference(sqlServerDbContainer);
//        break;

//    case DatabaseProvider.PostgresSQLContainer:
//        var postgresDbContainer = builder.AddPostgres(Constants.Infrastructure.PostgressContainerName).AddDatabase(ApplicationDbContext.DatabaseName);
//        webApp.WithReference(postgresDbContainer);
//        break;

//    default:
//        // Connect to external non-aspire DB.
//        break;
//}

//webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_Accounts>("AdminAssistant.Services.Accounts"));
//webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>("AdminAssistant.Services.AssetRegister"));
//webApp.WithReference(builder.AddProject<Projects.AdminAssistant_Services_Core>("AdminAssistant.Services.Core"));
builder.AddProject<Projects.AdminAssistant_Services_Budget>("adminassistant.services.budget");
builder.AddProject<Projects.AdminAssistant_Services_Calendar>("adminassistant.services.calendar");
builder.AddProject<Projects.AdminAssistant_Services_Contacts>("adminassistant.services.contacts");
builder.AddProject<Projects.AdminAssistant_Services_Documents>("adminassistant.services.documents");
builder.AddProject<Projects.AdminAssistant_Services_Mail>("adminassistant.services.mail");
builder.AddProject<Projects.AdminAssistant_Services_Tasks>("adminassistant.services.tasks");
builder.AddProject<Projects.AdminAssistant_Services_Notes>("adminassistant.services.notes");
builder.AddProject<Projects.AdminAssistant_Services_Admin>("adminassistant.services.admin");
builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>("adminassistant.services.scheduledpayments");
builder.AddProject<Projects.AdminAssistant_Gateway>("adminassistant.gateway");
builder.AddProject<Projects.AdminAssistant_Hangfire>("adminassistant.hangfire");

builder.Build().Run();
