using SimonGeering.Framework.TypeMapping;
using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public sealed record CurrencyResponseDto : IMapFrom<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
