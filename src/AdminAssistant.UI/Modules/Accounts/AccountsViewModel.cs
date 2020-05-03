using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.Accounts
{
    public class AccountsViewModel : ViewModelBase, IAccountsViewModel
    {
        public AccountsViewModel(ILoggingProvider log)
            : base(log)
        {
        }

        public string HeaderText { get; } = "Accounts";
        public string SubHeaderText { get; } = string.Empty;

        public BankAccount? SelectedBankAccount { get; }
    }
}
