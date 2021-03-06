using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    public class BankAccountInfoResponseDto : IMapFrom<BankAccountInfo>
    {
        public int BankAccountID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int CurrentBalance { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
        public bool IsBudgeted { get; set; }
    }
}
