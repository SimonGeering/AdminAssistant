namespace AdminAssistant.WebAPI.v1.CoreModule;

[SwaggerSchema(Required = ["CurrencyID", "Symbol", "DecimalFormat"])]
public sealed record CurrencyUpdateRequestDto
{
    [SwaggerSchema("The Currency identifier.", ReadOnly = true)]
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
