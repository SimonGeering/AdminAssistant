namespace AdminAssistant.DomainModel.Modules.ContactsModule;

public sealed record ContactId(int Value) : Id(Value)
{
    public static ContactId Default => new(Constants.UnknownRecordID);
}
