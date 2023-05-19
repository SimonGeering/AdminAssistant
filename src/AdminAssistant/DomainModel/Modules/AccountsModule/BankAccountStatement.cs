namespace AdminAssistant.DomainModel.Modules.AccountsModule;

/// <summary></summary>
/// <seealso cref="IDatabasePersistable"/>
public sealed record BankAccountStatement : IDatabasePersistable
{
    public int BankAccountStatementID { get; init; } = Constants.UnknownRecordID;
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
    public int PrimaryKey => BankAccountStatementID;
}
