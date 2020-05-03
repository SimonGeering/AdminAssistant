using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountRightSidebar
{
    public class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
    {
        private readonly IAccountsStateStore accountsStateStore;

        public BankAccountRightSidebarViewModel(IAccountsStateStore accountsStateStore, ILoggingProvider log)
            : base(log)
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
