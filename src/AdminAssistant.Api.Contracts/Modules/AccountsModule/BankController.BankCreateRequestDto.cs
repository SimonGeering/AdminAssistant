namespace AdminAssistant.WebAPI.v1.AccountsModule;

/// <summary>
/// Request DTO for creating a new bank.
/// </summary>
public sealed record BankCreateRequestDto
{
    /// <summary>
    /// The name of the bank. Required.
    /// </summary>
    [Required]
    public string BankName { get; init; } = string.Empty;
}
