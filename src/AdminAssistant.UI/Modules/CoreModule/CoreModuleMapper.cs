using AdminAssistant.WebAPI.v1.CoreModule;

namespace AdminAssistant.Modules.CoreModule;

internal static class CoreModuleMapper
{
    internal static IEnumerable<Currency> ToCurrencyList(this IEnumerable<CurrencyResponseDto> source)
        => source.Select(ToCurrency);

    private static Currency ToCurrency(this CurrencyResponseDto source)
        => new()
        {
            CurrencyID = new CurrencyId(source.CurrencyID),
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat,
        };
}
