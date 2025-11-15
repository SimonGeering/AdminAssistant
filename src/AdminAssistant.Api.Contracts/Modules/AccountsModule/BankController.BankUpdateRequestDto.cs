namespace AdminAssistant.WebAPI.v1.AccountsModule;

/// <summary>
/// Request DTO for updating an existing bank.
/// </summary>
public sealed record BankUpdateRequestDto
{
    /// <summary>
    /// The Bank identifier. Required. Read-only.
    /// </summary>
    [Required]
    [ReadOnly(true)]
    public int BankID { get; init; }

    /// <summary>
    /// The name of the bank. Required.
    /// </summary>
    [Required]
    public string BankName { get; init; } = string.Empty;
}
