namespace AdminAssistant.DomainModel.Shared
{
    public record ConfigurationSettings
    {
        public string DatabaseProvider { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
