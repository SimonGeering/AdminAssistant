namespace AdminAssistant.DomainModel.Modules.ContactsModule.Builders;

public interface IContactBuilder
{
    Contact Build();
    IContactBuilder WithTestData(int contactID = Constants.UnknownRecordID);
    IContactBuilder WithFirstName(string firstName);
    IContactBuilder WithLastName(string lastName);
}
