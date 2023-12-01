using AdminAssistant.Modules.ContactsModule;

namespace AdminAssistant.Modules.AccountsModule;

public sealed record PayeeContact : IPersistable
{
    public PayeeContactId PayeeContactID { get; init; } = PayeeContactId.Default;
    public PayeeId PayeeID { get; init; } = PayeeId.Default;
    public ContactId ContactID { get; init; } = ContactId.Default;
    public bool IsPrimaryContact { get; init; }

    public Id PrimaryKey => PayeeContactID;
}
public sealed record PayeeContactId(int Value) : Id(Value)
{
    public static PayeeContactId Default => new(Constants.UnknownRecordID);
}
