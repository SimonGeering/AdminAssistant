namespace AdminAssistant.DomainModel.Shared;

public interface IUserContextProvider
{
    User GetCurrentUser();
}
