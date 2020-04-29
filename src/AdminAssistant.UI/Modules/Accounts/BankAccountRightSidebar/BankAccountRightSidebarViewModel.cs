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
#if DEBUG
            System.Console.WriteLine("BankAccountRightSidebarViewModel => OnAddAccountButtonClick");
#endif
            this.accountsStateStore.OnCreateAccount();
        }
    }
}
