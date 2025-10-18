namespace AdminAssistant.Modules.CoreModule;

internal static class CoreModuleMapper
{
    internal static IEnumerable<Currency> ToCurrencyList(this ICollection<CurrencyResponseDto> source)
        => source.Select(ToCurrency);

    private static Currency ToCurrency(this CurrencyResponseDto source)
        => new()
        {
            CurrencyID = source.CurrencyID.HasValue ? new CurrencyId(source.CurrencyID.Value) : CurrencyId.Default,
            Symbol = source.Symbol ?? string.Empty,
            DecimalFormat = source.DecimalFormat ?? string.Empty,
        };
}
