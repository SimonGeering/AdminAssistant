using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public BankAccountEditDialogViewModel(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount BankAccount { get; set; }

        public void NewAccount()
        {
        }
    }
}
