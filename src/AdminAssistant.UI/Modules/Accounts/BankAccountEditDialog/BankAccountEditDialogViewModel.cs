using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using Ardalis.GuardClauses;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        private readonly IAccountsStateStore accountsStateStore;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountEditDialogViewModel(
            IAccountsStateStore accountsStateStore,
            IBankAccountValidator bankAccountValidator)
        {
            this.accountsStateStore = accountsStateStore;
            this.bankAccountValidator = bankAccountValidator;

            this.accountsStateStore.EditAccount += (BankAccount bankAccount) =>
            {
                Guard.Against.Null(bankAccount, nameof(bankAccount));

                this.BankAccount = bankAccount;

                this.HeaderText = this.BankAccount.BankAccountID == Constants.NewRecordID ? NewBankAccountHeader : EditBankAccountHeader;
                this.ShowDialog = true;
            };
        }

        public string HeaderText { get; private set; } = string.Empty;

        private bool showDialog = false;
        public bool ShowDialog
        {
            get { return this.showDialog; }
            set
            {
                if (this.showDialog == value)
                    return;

                this.showDialog = value;
                this.OnPropertyChanged();
            }
        }
        public BankAccount BankAccount { get; set; } = new BankAccount();

        public void OnCancelButtonClick()
        {
            this.ShowDialog = false;
        }

        public void OnSaveButtonClick()
        {
            this.ShowDialog = false;
        }
    }
}
