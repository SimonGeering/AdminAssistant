using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.Core.API;

[SwaggerSchema(Required = new[] { "Symbol", "DecimalFormat" })]
public record CurrencyCreateRequestDto : IMapTo<Currency>
{
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<CurrencyCreateRequestDto, Currency>()
                  .ForMember(x => x.CurrencyID, opt => opt.Ignore());
}
