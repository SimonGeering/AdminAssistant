using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.DAL.EntityFramework.Model.Core
{
    public class CurrencyEntity : IMapTo<Currency>
    {
        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
        public bool IsDeprecated { get; set; }
    }
}
