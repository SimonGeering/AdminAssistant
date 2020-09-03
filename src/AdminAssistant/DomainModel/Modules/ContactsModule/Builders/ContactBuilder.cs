namespace AdminAssistant.DomainModel.Modules.ContactsModule.Builders
{
    public interface IContactBuilder
    {
        Contact Build();
        IContactBuilder WithTestData(int assetID = Constants.UnknownRecordID);
        IContactBuilder WithFirstName(string firstName);
        IContactBuilder WithLastName(string lastName);
    }
    internal class ContactBuilder : Contact, IContactBuilder
    {
        public static Contact Default(IContactBuilder builder) => builder.Build();
        public static Contact Default(ContactBuilder builder) => builder.Build();

        public Contact Build() => this;

        public IContactBuilder WithTestData(int assetID = Constants.UnknownRecordID)
        {
            ContactID = assetID;
            FirstName = "Fred";
            LastName = "Blogs";
            return this;
        }

        public IContactBuilder WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public IContactBuilder WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
    }
}
