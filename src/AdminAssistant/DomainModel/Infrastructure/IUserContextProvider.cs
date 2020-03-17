namespace AdminAssistant.DomainModel.Infrastructure
{
    public interface IUserContextProvider
    {
        User GetCurrentUser();
    }
}
