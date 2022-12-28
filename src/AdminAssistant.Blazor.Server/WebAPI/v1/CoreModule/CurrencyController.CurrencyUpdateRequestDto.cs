using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;
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
