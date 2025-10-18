using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.WebAPI.v1.CoreModule;

public sealed record CurrencyResponseDto : IMapFrom<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
public static partial class CurrencyMapper
{
    public static CurrencyResponseDto ToCurrencyResponseDto(this Currency source)
        => new()
        {
            CurrencyID = source.CurrencyID.Value,
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat
        };
}
