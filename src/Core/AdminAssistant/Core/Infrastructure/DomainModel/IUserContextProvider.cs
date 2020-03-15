namespace AdminAssistant.Core.Infrastructure.DomainModel
{
    public interface IUserContextProvider
    {
        User GetCurrentUser();
    }
}
