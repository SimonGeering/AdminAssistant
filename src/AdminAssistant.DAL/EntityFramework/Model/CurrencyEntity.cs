using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    public class CurrencyEntity : IMapping<CurrencyEntity, Currency>
    {
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
        public bool IsDeprecated { get; set; }
    }
}
