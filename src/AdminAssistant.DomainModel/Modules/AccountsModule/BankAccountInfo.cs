namespace AdminAssistant.Modules.AccountsModule;

public sealed record BankAccountInfo : IPersistable
{
    public BankAccountId BankAccountID { get; init; } = BankAccountId.Default;
    public string AccountName { get; init; } = string.Empty;
    public int CurrentBalance { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }

    public Id PrimaryKey => BankAccountID;
}
