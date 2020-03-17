using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts
{
    public class AccountsViewModel : ViewModelBase, IAccountsViewModel
    {
        public string HeaderText { get; } = "Accounts";
        public string SubHeaderText { get; } = string.Empty;

        public BankAccount? SelectedBankAccount { get; }
    }
}
