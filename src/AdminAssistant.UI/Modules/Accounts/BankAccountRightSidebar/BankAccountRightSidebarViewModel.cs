using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountRightSidebar
{
    public class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
    {
        private readonly IAccountsStateStore accountsStateStore;

        public BankAccountRightSidebarViewModel(IAccountsStateStore accountsStateStore)
        {
            this.accountsStateStore = accountsStateStore;
        }

        public void OnAddAccountButtonClick()
        {
            this.accountsStateStore.OnEditAccount(new BankAccount());
        }
    }
}
