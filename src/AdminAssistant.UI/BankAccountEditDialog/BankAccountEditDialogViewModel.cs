using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Core.UI;

namespace AdminAssistant.Accounts.UI.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        bool ShowAccountEditDialog { get; }
        void NewAccount();
    }
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
