using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.CoreModule
{
    [SwaggerSchema(Required = new[] { "Symbol", "DecimalFormat" })]
    public class CurrencyCreateRequestDto : IMapTo<Currency>
    {
        [SwaggerSchema("The Currency identifier.", ReadOnly = true)]
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<CurrencyCreateRequestDto, Currency>()
                      .ForMember(x => x.CurrencyID, opt => opt.Ignore());
    }
}
