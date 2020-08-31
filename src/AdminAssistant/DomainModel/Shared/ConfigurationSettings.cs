namespace AdminAssistant.DomainModel.Infrastructure
{
    public class ConfigurationSettings
    {
        public string DatabaseProvider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
