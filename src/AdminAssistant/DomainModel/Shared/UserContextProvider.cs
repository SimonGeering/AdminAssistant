namespace AdminAssistant.DomainModel.Shared
{
    public interface IUserContextProvider
    {
        User GetCurrentUser();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class UserContextProvider : IUserContextProvider
    {
        // TODO: Implement IUserContextProvider.GetCurrentUser;
        // TODO: Hard coded user ID.
        public User GetCurrentUser() => new User() { UserID = 10, SignOn = "simongeering" };
    }
}
