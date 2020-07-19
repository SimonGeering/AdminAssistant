using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountRightSidebar
{
    public class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
    {
        private readonly IAccountsStateStore accountsStateStore;

        public BankAccountRightSidebarViewModel(IAccountsStateStore accountsStateStore, ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {
            this.accountsStateStore = accountsStateStore;
        }

        public void OnAddAccountButtonClick()
        {
            this.Log.Start();

            this.accountsStateStore.OnEditAccount(new BankAccount());

            this.Log.Finish();
        }
    }
}
