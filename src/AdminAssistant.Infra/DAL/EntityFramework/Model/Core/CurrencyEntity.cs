using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core
{
    public class CurrencyEntity : IMapFrom<Currency>, IMapTo<Currency>
    {
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
        public bool IsDeprecated { get; set; }

        public void MapFrom(AutoMapper.Profile profile) => profile
            .CreateMap<Currency, CurrencyEntity>()
            .ForMember(x => x.IsDeprecated, opt => opt.Ignore());
    }
}
