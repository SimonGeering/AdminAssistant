namespace AdminAssistant.DomainModel.Shared;

public interface IUserContextProvider
{
    User GetCurrentUser();
}
internal class UserContextProvider : IUserContextProvider
{
    // TODO: Implement IUserContextProvider.GetCurrentUser;
    // TODO: Hard coded user ID.
    public User GetCurrentUser() => new() { UserID = 10, SignOn = "simongeering" };
}
