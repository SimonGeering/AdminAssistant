namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record PayeeContact : IDatabasePersistable
{
    public PayeeContactId PayeeContactID { get; init; } = PayeeContactId.Default;
    public int PayeeID { get; init; }
    public int ContactID { get; init; }
    public bool IsPrimaryContact { get; init; }

    public Id PrimaryKey => PayeeContactID;
}
public sealed record PayeeContactId(int Value) : Id(Value)
{
    public static PayeeContactId Default => new(Constants.UnknownRecordID);
}
