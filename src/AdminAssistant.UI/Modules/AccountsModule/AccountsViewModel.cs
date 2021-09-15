using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class AccountsViewModel : ViewModelBase, IAccountsViewModel
    {
        public AccountsViewModel(ILoggingProvider log) : base(log)
        {
        }

        public string HeaderText { get; } = "Accounts";
        public string SubHeaderText { get; } = string.Empty;

        public BankAccount? SelectedBankAccount { get; }
    }
}
