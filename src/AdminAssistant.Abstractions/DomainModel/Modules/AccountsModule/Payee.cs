namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record Payee : IDatabasePersistable
{
    public const int NameMaxLength = Constants.NameMaxLength;

    public PayeeId PayeeID { get; init; } = PayeeId.Default;
    public string Name { get; init; } = string.Empty;

    public Id PrimaryKey => PayeeID;
}
public sealed record PayeeId(int Value) : Id(Value)
{
    public static PayeeId Default => new(Constants.UnknownRecordID);
}
