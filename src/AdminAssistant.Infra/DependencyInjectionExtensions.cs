using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AdminAssistant.Framework.MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
#if DEBUG
        public static void AddAdminAssistantServerSideInfra(this IServiceCollection services, DbConnection connection)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlite(connection));
            AddAccountsDAL(services);
        }
#endif
        public static void AddAdminAssistantServerSideInfra(this IServiceCollection services, ConfigurationSettings configurationSettings)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

            // EF Core ...
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsBuilder =>
            {
                if (Enum.TryParse(configurationSettings.DatabaseProvider, out DatabaseProvider databaseProvider) == false)
                    throw new Exception("Unable to load 'DatabaseProvider' configuration setting.");

                // This does not use GetConnectionString as KeyVault does not make the distinction.
                // All secrets are key value pairs, here the key is the DB provider ...
                var connectionString = configurationSettings.ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception("Configuration failed to load");

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

            AddAccountsDAL(services);
            AddCoreDAL(services);
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
}

