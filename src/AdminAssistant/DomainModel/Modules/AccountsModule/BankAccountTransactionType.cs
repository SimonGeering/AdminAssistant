namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountTransactionType : IDatabasePersistable
{
    public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

    public int BankAccountTransactionTypeID { get; init; } = Constants.UnknownRecordID;
    public string Description { get; init; } = string.Empty;
    public int PrimaryKey => BankAccountTransactionTypeID;
}
