using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public class BankAccountEditDialogViewModel : ViewModelBase, IBankAccountEditDialogViewModel
    {
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount BankAccount { get; set; } = new BankAccount();

        public void NewAccount()
        {
        }
    }
}
