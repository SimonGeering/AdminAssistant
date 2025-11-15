namespace AdminAssistant.WebAPI.v1.AccountsModule;

/// <summary>
/// Response DTO representing a bank account.
/// </summary>
public sealed record BankAccountResponseDto
{
    /// <summary>
    /// The BankAccount identifier. Read-only.
    /// </summary>
    [ReadOnly(true)]
    public int BankAccountID { get; init; }

    /// <summary>
    /// The BankAccountType for this BankAccount.
    /// </summary>
    public int BankAccountTypeID { get; init; }

    /// <summary>
    /// The identifier of the currency associated with this account.
    /// </summary>
    public int CurrencyID { get; init; }

    /// <summary>
    /// The name of the account.
    /// </summary>
    public string AccountName { get; init; } = string.Empty;

    /// <summary>
    /// Indicates whether the account is budgeted.
    /// </summary>
    public bool IsBudgeted { get; init; }

    /// <summary>
    /// The opening balance of the account.
    /// </summary>
    public int OpeningBalance { get; init; }

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public int CurrentBalance { get; init; }

    /// <summary>
    /// The date the account was opened.
    /// </summary>
    public DateTime OpenedOn { get; init; }
}
