namespace AdminAssistant.DomainModel.Modules.ContactsModule.Builders
{
    public interface IContactBuilder
    {
        Contact Build();
        IContactBuilder WithTestData(int assetID = Constants.UnknownRecordID);
        IContactBuilder WithFirstName(string firstName);
        IContactBuilder WithLastName(string lastName);
    }
    internal class ContactBuilder : IContactBuilder
    {
        private Contact _contact = new();

        public static Contact Default(IContactBuilder builder) => builder.Build();
        public static Contact Default(ContactBuilder builder) => builder.Build();

        public Contact Build() => _contact;

        public IContactBuilder WithTestData(int assetID = Constants.UnknownRecordID)
        {
            _contact = _contact with
            {
                ContactID = assetID,
                FirstName = "Fred",
                LastName = "Blogs"
            };
            return this;
        }

        public IContactBuilder WithFirstName(string firstName)
        {
            _contact = _contact with { FirstName = firstName };
            return this;
        }

        public IContactBuilder WithLastName(string lastName)
        {
            _contact = _contact with { LastName = lastName };
            return this;
        }
    }
}
