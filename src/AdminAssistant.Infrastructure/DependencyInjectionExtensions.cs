using AdminAssistant;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.MediatR;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Modules.DocumentsModule.Infrastructure.DAL;
using AdminAssistant.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using PdfSharp.Charting;
using SimonGeering.Framework.Configuration;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AdminAssistant.Test")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{ 
    public static void AddAdminAssistantServerSideInfra(this IHostApplicationBuilder builder, ConfigurationSettings configurationSettings)
    {
        var databaseProvider = configurationSettings.GetDatabaseProviderRequiredSetting();

        switch (databaseProvider)
        {
            case DatabaseProvider.SQLServerContainer:
                builder.AddSqlServerDbContext<ApplicationDbContext>(ApplicationDbContext.DatabaseName);
                break;

            case DatabaseProvider.PostgresSQLContainer:
                builder.AddNpgsqlDbContext<ApplicationDbContext>(ApplicationDbContext.DatabaseName);
                break;

            default:
                builder.Services.AddNonContainerDbContext(configurationSettings);
                break;
        }
        builder.Services.AddAdminAssistantServerSideInfra();
    }

    public static void AddAdminAssistantServerSideInfra(this IServiceCollection services, ConfigurationSettings configurationSettings)
    {
        services.AddNonContainerDbContext(configurationSettings);
        services.AddAdminAssistantServerSideInfra();
    }

    private static DatabaseProvider GetDatabaseProviderRequiredSetting(this ConfigurationSettings configurationSettings)
    {
        if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
            throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

        return databaseProvider;
    }

    private static void AddNonContainerDbContext(this IServiceCollection services, ConfigurationSettings configurationSettings)
    {
        // This does not use GetConnectionString as KeyVault does not make the distinction.
        // All secrets are key value pairs, here the key is the DB provider ...
        var connectionString = configurationSettings.ConnectionString;

        if (string.IsNullOrEmpty(connectionString))
            throw new ConfigurationException("Configuration failed to load");

        var databaseProvider = configurationSettings.GetDatabaseProviderRequiredSetting();
        switch (databaseProvider)
        {
            case DatabaseProvider.SQLServer:
            case DatabaseProvider.SQLServerLocalDB:
                services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
                break;

            case DatabaseProvider.SQLite:
                services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite(connectionString));
                break;

            case DatabaseProvider.PostgresSQL:
                services.AddDbContext<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
                break;

            default:
                throw new UnsupportedDatabaseProviderException(databaseProvider);
        }
    }

    private static void AddAdminAssistantServerSideInfra(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>
        AddDALRepositories(services);
    }

    public static void AddAdminAssistantServerSideProviders(this IServiceCollection services)
    {
        services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
        services.AddSharedProviders();
    }

    private static void AddDALRepositories(this IServiceCollection services)
    {
        AddAccountsDAL(services);
        AddContactsDAL(services);
        AddCoreDAL(services);
        AddDocumentsDAL(services);
    }

    private static void AddAccountsDAL(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountInfoRepository, BankAccountInfoRepository>();
        services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        services.AddTransient<IBankAccountTransactionRepository, BankAccountTransactionRepository>();
        services.AddTransient<IBankAccountTypeRepository, BankAccountTypeRepository>();
        services.AddTransient<IBankRepository, BankRepository>();
    }

    private static void AddContactsDAL(this IServiceCollection services)
        => services.AddTransient<IContactRepository, ContactRepository>();

    private static void AddCoreDAL(this IServiceCollection services)
        => services.AddTransient<ICurrencyRepository, CurrencyRepository>();

    private static void AddDocumentsDAL(this IServiceCollection services)
        => services.AddTransient<IDocumentRepository, DocumentRepository>();
}
