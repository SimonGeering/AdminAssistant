namespace AdminAssistant.DomainModel.Infrastructure
{
    public class UserContextProvider : IUserContextProvider
    {
        public User GetCurrentUser()
        {
            // TODO: Implement IUserContextProvider.GetCurrentUser;
            // TODO: Hard coded user ID.
            return new User() { UserID = 10, SignOn = "simongeering" };
        }
    }
}
