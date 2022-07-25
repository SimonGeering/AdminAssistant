using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Core.API;

public record CurrencyResponseDto : IMapFrom<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
