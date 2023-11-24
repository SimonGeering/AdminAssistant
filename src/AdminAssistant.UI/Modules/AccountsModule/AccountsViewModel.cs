using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class AccountsViewModel(ILoggingProvider log)
    : ViewModelBase(log), IAccountsViewModel
{
    public string HeaderText { get; } = "Accounts";
    public string SubHeaderText { get; } = string.Empty;

    public BankAccount? SelectedBankAccount { get; }
}
