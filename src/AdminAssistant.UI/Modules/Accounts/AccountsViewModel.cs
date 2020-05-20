using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.Accounts
{
    public class AccountsViewModel : ViewModelBase, IAccountsViewModel
    {
        public AccountsViewModel(ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {
        }

        public string HeaderText { get; } = "Accounts";
        public string SubHeaderText { get; } = string.Empty;

        public BankAccount? SelectedBankAccount { get; }
    }
}
