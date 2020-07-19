using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountBalanceList
{
    public class BankAccountBalanceListViewModel : ViewModelBase, IBankAccountBalanceListViewModel
    {
        public BankAccountBalanceListViewModel(ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {

        }
    }
}
