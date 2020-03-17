namespace AdminAssistant.DomainModel.Infrastructure
{
    public class UserContextProvider : IUserContextProvider
    {
        public User GetCurrentUser()
        {
            // TODO: Implement IUserContextProvider.GetCurrentUser;
            return new User() { SignOn = "simongeering" };
        }
    }
}
