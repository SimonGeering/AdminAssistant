using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.CoreModule
{
    [SwaggerSchema(Required = new[] { "CurrencyID", "Symbol", "DecimalFormat" })]
    public class CurrencyUpdateRequestDto : IMapTo<Currency>
    {
        [SwaggerSchema("The Currency identifier.", ReadOnly = true)]
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<CurrencyUpdateRequestDto, Currency>()
                      .ForMember(x => x.CurrencyID, opt => opt.Ignore());
    }
}
