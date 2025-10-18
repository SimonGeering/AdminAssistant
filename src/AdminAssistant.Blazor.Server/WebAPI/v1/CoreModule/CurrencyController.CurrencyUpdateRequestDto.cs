using AdminAssistant.Modules.CoreModule;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.CoreModule;

[SwaggerSchema(Required = new[] { "CurrencyID", "Symbol", "DecimalFormat" })]
public sealed record CurrencyUpdateRequestDto : IMapTo<Currency>
{
    [SwaggerSchema("The Currency identifier.", ReadOnly = true)]
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
public static partial class CurrencyMapper
{
    public static CurrencyUpdateRequestDto ToCurrencyUpdateRequest(this Currency source)
        => new()
        {
            CurrencyID = source.CurrencyID.Value,
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat
        };

    public static Currency ToCurrency(this CurrencyUpdateRequestDto source)
        => new()
        {
            CurrencyID = new CurrencyId(source.CurrencyID),
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat
        };
}
