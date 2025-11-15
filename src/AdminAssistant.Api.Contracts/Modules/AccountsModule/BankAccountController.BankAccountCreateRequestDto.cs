namespace AdminAssistant.WebAPI.v1.AccountsModule;

/// <summary>
/// Request DTO for creating a new bank account.
/// </summary>
public sealed record BankAccountCreateRequestDto
{
    /// <summary>
    /// The BankAccountType for this BankAccount. Required.
    /// </summary>
    [Required]
    public int BankAccountTypeID { get; init; }

    /// <summary>
    /// The identifier of the currency associated with this account. Required.
    /// </summary>
    [Required]
    public int CurrencyID { get; init; }

    /// <summary>
    /// The name of the account. Required.
    /// </summary>
    [Required]
    public string AccountName { get; init; } = string.Empty;

    /// <summary>
    /// The balance of the account.
    /// </summary>
    public int Balance { get; init; }

    /// <summary>
    /// Indicates whether the account is budgeted. Required.
    /// </summary>
    [Required]
    public bool IsBudgeted { get; init; }

    /// <summary>
    /// The opening balance of the account. Required.
    /// </summary>
    [Required]
    public int OpeningBalance { get; init; }

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public int CurrentBalance { get; init; }

    /// <summary>
    /// The date the account was opened. Required.
    /// </summary>
    [Required]
    public DateTime OpenedOn { get; init; }
}
