using System.Diagnostics.CodeAnalysis;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.DomainModel.Shared;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AdminAssistant.Framework.MediatR;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.DAL.Modules.DocumentsModule;
using AdminAssistant.Framework.Configuration;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AdminAssistant.Test")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantServerSideInfra(this IServiceCollection services, ConfigurationSettings configurationSettings)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

        // EF Core ...
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsBuilder =>
        {
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
                    optionsBuilder.UseSqlServer(connectionString);
                    break;

                case DatabaseProvider.SQLServerLocalDB:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;

                case DatabaseProvider.SQLite:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
            }
        });
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
        AddCoreDAL(services);
        AddDocumentsDAL(services);
    }

    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "WIP")]
    private static void AddDocumentsDAL(this IServiceCollection services)
    {
        services.AddTransient<IDocumentRepository, DocumentRepository>();
    }

    private static void AddAccountsDAL(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountInfoRepository, BankAccountInfoRepository>();
        services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        services.AddTransient<IBankAccountTransactionRepository, BankAccountTransactionRepository>();
        services.AddTransient<IBankAccountTypeRepository, BankAccountTypeRepository>();
        services.AddTransient<IBankRepository, BankRepository>();
    }

    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "WIP")]
    private static void AddCoreDAL(this IServiceCollection services)
    {
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
    }
}

