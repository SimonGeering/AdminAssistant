namespace AdminAssistant.DomainModel.Infrastructure
{
    public class DatabaseSettings
    {
        public DatabaseSettings(string connectionString, DatabaseProvider databaseProvider)
        {
            this.ConnectionString = connectionString;
            this.DatabaseProvider = databaseProvider;
        }
        public string ConnectionString { get; private set; }
        public DatabaseProvider DatabaseProvider { get; private set; }
    }
}
