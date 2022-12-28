using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.CoreModule;

public sealed record CurrencyResponseDto : IMapFrom<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
