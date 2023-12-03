using AdminAssistant.Modules.CoreModule;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.CoreModule;

[SwaggerSchema(Required = new[] { "Symbol", "DecimalFormat" })]
public sealed record CurrencyCreateRequestDto : IMapTo<Currency>
{
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<CurrencyCreateRequestDto, Currency>()
                  .ForMember(x => x.CurrencyID, opt => opt.Ignore());
}
