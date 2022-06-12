namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public record BankAccountInfo : IDatabasePersistable
{
    public int BankAccountID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public int CurrentBalance { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }

    public int PrimaryKey => BankAccountID;
}
