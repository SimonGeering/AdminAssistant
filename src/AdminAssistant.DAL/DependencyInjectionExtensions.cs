using System;
using System.Data.Common;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.DomainModel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
#if DEBUG
        public static void AddAdminAssistantServerSideDAL(this IServiceCollection services, DbConnection connection)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(connection);
            });
            AddAccountsDAL(services);
        }
#endif
        public static void AddAdminAssistantServerSideDAL(this IServiceCollection services, IConfiguration configuration)
        {
            // EF Core ...
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsBuilder =>
            {
                if (Enum.TryParse(configuration["DATABASE_PROVIDER"], out DatabaseProvider databaseProvider) == false)
                    throw new Exception("Unable to load 'DATABASE_PROVIDER' application setting.");

                // This does not use GetConnectionString as KeyVault does not make the distinction.
                // All secrets are key value pairs, here the key is the DB provider ...
                var connectionString = configuration[$"{databaseProvider}"];

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
        }

        private static void AddAccountsDAL(this IServiceCollection services)
        {
            // Repositories ...
            services.AddTransient<IBankAccountTypeRepository, BankAccountTypeRepository>();
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        }
    }
}

