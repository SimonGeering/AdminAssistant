using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class AccountsViewModel : ViewModelBase, IAccountsViewModel
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
