namespace AdminAssistant.Core.Infrastructure.DomainModel
{
    public interface IConfigurationManager
    {
        DatabaseSettings GetDatabaseSettings();
    }    
}
