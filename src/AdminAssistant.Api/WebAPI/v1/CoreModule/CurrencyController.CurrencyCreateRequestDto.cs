namespace AdminAssistant.WebAPI.v1.CoreModule;

[SwaggerSchema(Required = ["Symbol", "DecimalFormat"])]
public sealed record CurrencyCreateRequestDto
{
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
