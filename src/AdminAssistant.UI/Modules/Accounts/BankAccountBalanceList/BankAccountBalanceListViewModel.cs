using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountBalanceList
{
    public class BankAccountBalanceListViewModel : ViewModelBase, IBankAccountBalanceListViewModel
    {
        public BankAccountBalanceListViewModel(ILoggingProvider log)
            : base(log)
        {

        }
    }
}
