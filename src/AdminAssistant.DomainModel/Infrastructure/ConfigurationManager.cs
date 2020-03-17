using System;
using Microsoft.Extensions.Configuration;

namespace AdminAssistant.Core.Infrastructure.DomainModel
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            // TODO: Add validator to check for null values and throw exception.
            this.configuration = configuration;
        }

        public DatabaseSettings GetDatabaseSettings()
        {
            if (Enum.TryParse(this.configuration["DATABASE_PROVIDER"], out DatabaseProvider databaseProvider) == false)
                throw new Exception("Unable to load 'DATABASE_PROVIDER' application setting.");

            // This does not use GetConnectionString as KeyVault does not make the distinction.
            // All secrets are key value pairs, here the key is the DB provider ...
            var connectionString = this.configuration[$"{databaseProvider}"];

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Configuration failed to load");

            return new DatabaseSettings(connectionString, databaseProvider);
        }
    }
}
