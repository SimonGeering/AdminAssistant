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
using Microsoft.Extensions.Hosting;
using SimonGeering.Framework.Configuration;

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
            case DatabaseProvider.SQLServerLocalDB:
                AddSqlServerDALRepositories(services);
                break;

            case DatabaseProvider.SQLite:
                throw new NotImplementedException();
                //AddSqliteDALRepositories(services);
                //break;

            case DatabaseProvider.PostgresSQL:
                AddPostgresDALRepositories(services);
                break;

            case DatabaseProvider.SQLServerContainer:
                AddSqlServerDALRepositories(services);
                break;

            case DatabaseProvider.PostgresSQLContainer:
                AddPostgresDALRepositories(services);
                break;
        }
    }

    public static void AddAdminAssistantApplicationDbContext(this IHostApplicationBuilder builder, ConfigurationSettings configurationSettings)
    {
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

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
            case DatabaseProvider.SQLServerLocalDB:
                builder.Services.AddDbContext<IApplicationDbContext, SqlServerApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
                break;

            case DatabaseProvider.SQLite:
                builder.Services.AddDbContext<IApplicationDbContext, SqliteApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite(connectionString));
                throw new NotImplementedException();
            //AddSqliteDALRepositories(builder.Services);
            //break;

            case DatabaseProvider.PostgresSQL:
                builder.Services.AddDbContext<IApplicationDbContext, PostgresApplicationDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
                break;

            case DatabaseProvider.SQLServerContainer:
                builder.AddSqlServerDbContext<SqlServerApplicationDbContext>(configurationSettings.AspireDatabaseName);
                break;

            case DatabaseProvider.PostgresSQLContainer:
                builder.AddNpgsqlDbContext<PostgresApplicationDbContext>(configurationSettings.AspireDatabaseName);
                break;
        }
    }

    public static void AddAdminAssistantServerSideProviders(this IServiceCollection services)
    {
        services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
        services.AddSharedProviders();
    }

    private static void AddSqlServerDALRepositories(this IServiceCollection services)
    {
        AddSqlServerAccountsDAL(services);
        AddSqlServerContactsDAL(services);
        AddSqlServerCoreDAL(services);
        AddSqlServerDocumentsDAL(services);
    }

    private static void AddSqlServerAccountsDAL(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountInfoRepository, SqlServerBankAccountInfoRepository>();
        services.AddTransient<IBankAccountRepository, SqlServerBankAccountRepository>();
        services.AddTransient<IBankAccountTransactionRepository, SqlServerBankAccountTransactionRepository>();
        services.AddTransient<IBankAccountTypeRepository, SqlServerBankAccountTypeRepository>();
        services.AddTransient<IBankRepository, SqlServerBankRepository>();
    }

    private static void AddSqlServerContactsDAL(this IServiceCollection services)
        => services.AddTransient<IContactRepository, SqlServerContactRepository>();

    private static void AddSqlServerCoreDAL(this IServiceCollection services)
        => services.AddTransient<ICurrencyRepository, SqlServerCurrencyRepository>();

    private static void AddSqlServerDocumentsDAL(this IServiceCollection services)
        => services.AddTransient<IDocumentRepository, SqlServerDocumentRepository>();

    private static void AddPostgresDALRepositories(this IServiceCollection services)
    {
        AddPostgresAccountsDAL(services);
        AddPostgresContactsDAL(services);
        AddPostgresCoreDAL(services);
        AddPostgresDocumentsDAL(services);
    }

    private static void AddPostgresAccountsDAL(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountInfoRepository, PostgresBankAccountInfoRepository>();
        services.AddTransient<IBankAccountRepository, PostgresBankAccountRepository>();
        services.AddTransient<IBankAccountTransactionRepository, PostgresBankAccountTransactionRepository>();
        services.AddTransient<IBankAccountTypeRepository, PostgresBankAccountTypeRepository>();
        services.AddTransient<IBankRepository, PostgresBankRepository>();
    }

    private static void AddPostgresContactsDAL(this IServiceCollection services)
        => services.AddTransient<IContactRepository, PostgresContactRepository>();

    private static void AddPostgresCoreDAL(this IServiceCollection services)
        => services.AddTransient<ICurrencyRepository, PostgresCurrencyRepository>();


    private static void AddPostgresDocumentsDAL(this IServiceCollection services)
        => services.AddTransient<IDocumentRepository, PostgresDocumentRepository>();
}
