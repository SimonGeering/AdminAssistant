using AdminAssistant.Shared;

namespace AdminAssistant.Modules.AccountsModule;

public sealed record Bank : IPersistable
{
    public const int BankNameMaxLength = EntityName.MaxLength;

    public BankId BankID { get; init; } = BankId.Default;
    public BankName BankName { get; init; } = BankName.Default;

    ///<inheritdoc/>
    public Id PrimaryKey => BankID;
}
public sealed record BankId(int Value) : Id(Value)
{
    public static BankId Default => new(Constants.UnknownRecordID);
}
public sealed record BankName(string Value) : EntityName(Value)
{
    public static BankName Default => new(string.Empty);
}
