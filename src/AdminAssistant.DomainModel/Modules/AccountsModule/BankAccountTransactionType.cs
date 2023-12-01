using AdminAssistant.Shared;

namespace AdminAssistant.Modules.AccountsModule;

public sealed record BankAccountTransactionType : IPersistable
{
    public const int DescriptionMaxLength = EntityDescription.MaxLength;

    public BankAccountTransactionTypeId BankAccountTransactionTypeID { get; init; } = BankAccountTransactionTypeId.Default;
    public string Description { get; init; } = string.Empty;
    public Id PrimaryKey => BankAccountTransactionTypeID;
}
public sealed record BankAccountTransactionTypeId(int Value) : Id(Value)
{
    public static BankAccountTransactionTypeId Default => new(Constants.UnknownRecordID);
}
