namespace AdminAssistant.WebAPI.v1.AccountsModule;

[SwaggerSchema(Required = new[] { "BankAccountTypeID", "CurrencyID", "AccountName", "IsBudgeted", "OpeningBalance", "OpenedOn" })]
public sealed record BankAccountCreateRequestDto
{
    [SwaggerSchema("The BankAccountType for this BankAccount.")]
    public int BankAccountTypeID { get; init; }
    public int CurrencyID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public int Balance { get; init; }
    public bool IsBudgeted { get; init; }
    public int OpeningBalance { get; init; }
    public int CurrentBalance { get; init; }
    public DateTime OpenedOn { get; init; }
}
