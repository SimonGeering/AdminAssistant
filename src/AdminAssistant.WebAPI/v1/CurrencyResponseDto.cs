using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1
{
    public class CurrencyResponseDto : IMapFrom<Currency>, IMapTo<Currency>
    {
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
    }
}
