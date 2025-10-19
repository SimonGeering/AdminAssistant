using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.WebAPI.v1.CoreModule;

public static class CoreModuleMapper
{
    public static Currency ToCurrency(this CurrencyCreateRequestDto source)
        => new()
        {
            CurrencyID = CurrencyId.Default,
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

    public static CurrencyResponseDto ToCurrencyResponseDto(this Currency source)
        => new()
        {
            CurrencyID = source.CurrencyID.Value,
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat
        };

    public static IEnumerable<CurrencyResponseDto> ToCurrencyResponseDtoEnumeration(this IEnumerable<Currency> source)
        => source.Select(x => x.ToCurrencyResponseDto());

    public static CurrencyUpdateRequestDto ToCurrencyUpdateRequestDto(this Currency source)
        => new()
        {
            CurrencyID = source.CurrencyID.Value,
            Symbol = source.Symbol,
            DecimalFormat = source.DecimalFormat
        };
}
