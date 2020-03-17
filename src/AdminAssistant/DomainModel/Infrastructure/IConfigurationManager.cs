namespace AdminAssistant.DomainModel.Infrastructure
{
    public interface IConfigurationManager
    {
        DatabaseSettings GetDatabaseSettings();
    }    
}
