namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountTransaction : IDatabasePersistable
{
    public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

    public BankAccountTransactionId BankAccountTransactionID { get; init; } = BankAccountTransactionId.Default;
    public int BankAccountID { get; init; }
    public int BankAccountTransactionTypeID { get; init; }
    public int BankAccountStatementID { get; init; }
    public int BankAccountStatementNumber { get; init; }
    public bool IsReconciled { get; init; }

    public int PayeeID { get; init; }
    public string PayeeName { get; init; } = string.Empty;

    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public int Credit { get; init; }
    public int Debit { get; init; }
    public int Balance { get; init; }
    public string Description { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
    public DateTime TransactionDate { get; init; } = DateTime.Now;

    public Id PrimaryKey => BankAccountTransactionID;
}
public sealed record BankAccountTransactionId(int Value) : Id(Value)
{
    public static BankAccountTransactionId Default => new(Constants.UnknownRecordID);
}
