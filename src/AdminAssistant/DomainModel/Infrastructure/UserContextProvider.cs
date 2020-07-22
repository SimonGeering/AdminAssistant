namespace AdminAssistant.DomainModel.Infrastructure
{
    public interface IUserContextProvider
    {
        User GetCurrentUser();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class UserContextProvider : IUserContextProvider
    {
        public User GetCurrentUser()
        {
            // TODO: Implement IUserContextProvider.GetCurrentUser;
            // TODO: Hard coded user ID.
            return new User() { UserID = 10, SignOn = "simongeering" };
        }
    }
}