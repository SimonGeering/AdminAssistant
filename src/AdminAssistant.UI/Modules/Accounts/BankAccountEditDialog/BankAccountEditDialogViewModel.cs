using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        private readonly IAccountsStateStore accountsStateStore;

        public BankAccountEditDialogViewModel(IAccountsStateStore accountsStateStore)
        {
            this.accountsStateStore = accountsStateStore;

            this.accountsStateStore.CreateAccount += (BankAccount bankAccount) =>
            {
                this.BankAccount = bankAccount;
                this.ShowAccountEditDialog = true;
            };
        }

        private bool showAccountEditDialog = false;
        public bool ShowAccountEditDialog
        {
            get { return this.showAccountEditDialog; }
            set
            {
                if (this.showAccountEditDialog == value)
                    return;

                this.showAccountEditDialog = value;
                this.OnPropertyChanged();
            }
        }
        public BankAccount BankAccount { get; set; } = new BankAccount();

        public void OnCancelButtonClick()
        {
            this.ShowAccountEditDialog = false;
        }

        public void OnSaveButtonClick()
        {
            this.ShowAccountEditDialog = false;
        }
    }
}
