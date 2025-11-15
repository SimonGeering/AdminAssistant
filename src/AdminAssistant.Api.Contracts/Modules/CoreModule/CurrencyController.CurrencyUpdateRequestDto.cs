namespace AdminAssistant.WebAPI.v1.CoreModule;

/// <summary>
/// Request DTO for updating an existing currency.
/// </summary>
public sealed record CurrencyUpdateRequestDto
{
    /// <summary>
    /// The Currency identifier. Required. Read-only.
    /// </summary>
    [Required]
    [ReadOnly(true)]
    public int CurrencyID { get; init; }

    /// <summary>
    /// The symbol representing the currency (e.g., USD, GBP). Required.
    /// </summary>
    [Required]
    public string Symbol { get; init; } = string.Empty;

    /// <summary>
    /// The decimal format used for currency values (e.g., "0.00"). Required.
    /// </summary>
    [Required]
    public string DecimalFormat { get; init; } = string.Empty;
}
