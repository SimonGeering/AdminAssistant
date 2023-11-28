namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record Bank : IDatabasePersistable
{
    public const int BankNameMaxLength = Constants.NameMaxLength;

    public BankId BankID { get; init; } = BankId.Default;
    public string BankName { get; init; } = string.Empty;

    ///<inheritdoc/>
    public Id PrimaryKey => BankID;
}
public sealed record BankId(int Value) : Id(Value)
{
    public static BankId Default => new(Constants.UnknownRecordID);
}
