namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountTransactionType : IDatabasePersistable
{
    public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

    public BankAccountTransactionTypeId BankAccountTransactionTypeID { get; init; } = BankAccountTransactionTypeId.Default;
    public string Description { get; init; } = string.Empty;
    public Id PrimaryKey => BankAccountTransactionTypeID;
}
public sealed record BankAccountTransactionTypeId(int Value) : Id(Value)
{
    public static BankAccountTransactionTypeId Default => new(Constants.UnknownRecordID);
}
