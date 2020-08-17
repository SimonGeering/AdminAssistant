using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountRightSidebar
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
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
