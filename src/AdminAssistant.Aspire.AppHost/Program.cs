// https://devblogs.microsoft.com/dotnet/announcing-dotnet-aspire-preview-2/
// https://learn.microsoft.com/en-gb/dotnet/aspire/
using AdminAssistant;
//using AdminAssistant.Infrastructure.EntityFramework;
//using AdminAssistant.Shared;
//using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using SimonGeering.Framework.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Config Settings ...
//var configurationSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
//Guard.Against.Null(configurationSettings);

//if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
//    throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

// Services ...
var accounts = builder.AddProject<Projects.AdminAssistant_Services_Accounts>("AdminAssistant.Services.Accounts");
var admin = builder.AddProject<Projects.AdminAssistant_Services_Admin>("AdminAssistant.Services.Admin");
var assetRegister = builder.AddProject<Projects.AdminAssistant_Services_AssetRegister>("AdminAssistant.Services.AssetRegister");
var budget = builder.AddProject<Projects.AdminAssistant_Services_Budget>("AdminAssistant.Services.Budget");
var calendar = builder.AddProject<Projects.AdminAssistant_Services_Calendar>("AdminAssistant.Services.Calendar");
var contacts = builder.AddProject<Projects.AdminAssistant_Services_Contacts>("AdminAssistant.Services.Contacts");
var core = builder.AddProject<Projects.AdminAssistant_Services_Core>("AdminAssistant.Services.Core");
var documents = builder.AddProject<Projects.AdminAssistant_Services_Documents>("AdminAssistant.Services.Documents");
var mail = builder.AddProject<Projects.AdminAssistant_Services_Mail>("AdminAssistant.Services.Mail");
var notes = builder.AddProject<Projects.AdminAssistant_Services_Notes>("AdminAssistant.Services.Notes");
var scheduledPayments = builder.AddProject<Projects.AdminAssistant_Services_ScheduledPayments>("AdminAssistant.Services.ScheduledPayments");
var tasks = builder.AddProject<Projects.AdminAssistant_Services_Tasks>("AdminAssistant.Services.Tasks");

builder.AddProject<Projects.AdminAssistant_Hangfire>("AdminAssistant.HangFire");

var gateway = builder.AddProject<Projects.AdminAssistant_Gateway>("AdminAssistant.Gateway")
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

//var webApp = builder.AddProject<Projects.AdminAssistant_Blazor_Server>("AdminAssistant.Blazor.Server");
builder.AddProject<Projects.AdminAssistant_Blazor>("AdminAssistant.Blazor")
    .WithReference(gateway);

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


builder.Build().Run();
