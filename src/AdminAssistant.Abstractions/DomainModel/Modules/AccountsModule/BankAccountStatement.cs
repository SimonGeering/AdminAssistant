namespace AdminAssistant.DomainModel.Modules.AccountsModule;

/// <summary></summary>
/// <seealso cref="IDatabasePersistable"/>
public sealed record BankAccountStatement : IDatabasePersistable
{
    public BankAccountStatementId BankAccountStatementID { get; init; } = BankAccountStatementId.Default;
    public int BankAccountID { get; init; } = Constants.UnknownRecordID;
    public int DocumentID { get; init; } = Constants.UnknownRecordID;
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
