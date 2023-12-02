using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AdminAssistant.Framework.MediatR;
using SimonGeering.Framework.Configuration;
using AdminAssistant.Modules.DocumentsModule.Infrastructure.DAL;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AdminAssistant.Test")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantServerSideInfra(this IServiceCollection services, ConfigurationSettings configurationSettings)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

        // EF Core ...
        if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
            throw new ConfigurationException("Unable to load 'DatabaseProvider' configuration setting.");

        // This does not use GetConnectionString as KeyVault does not make the distinction.
        // All secrets are key value pairs, here the key is the DB provider ...
        var connectionString = configurationSettings.ConnectionString;

        if (string.IsNullOrEmpty(connectionString))
            throw new ConfigurationException("Configuration failed to load");

        switch (databaseProvider)
        {
            case DatabaseProvider.SQLServer:
                services.AddDbContext<IApplicationDbContext, SqlServerApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
                break;

            case DatabaseProvider.SQLServerLocalDB:
                services.AddDbContext<IApplicationDbContext, SqlServerApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
                break;

            case DatabaseProvider.SQLite:
                services.AddDbContext<IApplicationDbContext, SqliteApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite(connectionString));
                break;

            case DatabaseProvider.PostgresSQL:
                services.AddDbContext<IApplicationDbContext, PostgresApplicationDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
                break;
        }

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
