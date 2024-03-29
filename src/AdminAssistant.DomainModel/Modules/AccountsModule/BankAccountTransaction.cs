using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Shared;

namespace AdminAssistant.Modules.AccountsModule;

public sealed record BankAccountTransaction : IPersistable
{
    public const int DescriptionMaxLength = EntityDescription.MaxLength;

    public BankAccountTransactionId BankAccountTransactionID { get; init; } = BankAccountTransactionId.Default;
    public BankAccountId BankAccountID { get; init; } = BankAccountId.Default;
    public BankAccountTransactionTypeId BankAccountTransactionTypeID { get; init; } = BankAccountTransactionTypeId.Default;
    public BankAccountStatementId BankAccountStatementID { get; init; } = BankAccountStatementId.Default;
    public int BankAccountStatementNumber { get; init; }
    public bool IsReconciled { get; init; }

    public PayeeId PayeeID { get; init; } = PayeeId.Default;
    public string PayeeName { get; init; } = string.Empty;

    public CurrencyId CurrencyID { get; init; } = CurrencyId.Default;
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
