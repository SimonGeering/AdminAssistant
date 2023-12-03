using AdminAssistant.Modules.DocumentsModule;

namespace AdminAssistant.Modules.AccountsModule;

/// <summary></summary>
/// <seealso cref="IPersistable"/>
public sealed record BankAccountStatement : IPersistable
{
    public BankAccountStatementId BankAccountStatementID { get; init; } = BankAccountStatementId.Default;
    public BankAccountId BankAccountID { get; init; } = BankAccountId.Default;
    public DocumentId DocumentID { get; init; } = DocumentId.Default;
    public DateTime StatementDate { get; init; }
    public bool IsReconciled { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int OpeningBalance { get; init; }
    public int ClosingBalance { get; init; }
    public int TotalPaymentsIn { get; init; }
    public int TotalPaymentsOut { get; init; }

    /// <inheritdoc/>
    public Id PrimaryKey => BankAccountStatementID;
}
public sealed record BankAccountStatementId(int Value) : Id(Value)
{
    public static BankAccountStatementId Default => new(Constants.UnknownRecordID);
}
