namespace AdminAssistant.Shared;

public interface IUserContextProvider
{
    User GetCurrentUser();
}
internal sealed class UserContextProvider : IUserContextProvider
{
    // TODO: Hard coded user ID.
    public User GetCurrentUser() => new() { UserID = 10, SignOn = "simongeering" };
}
