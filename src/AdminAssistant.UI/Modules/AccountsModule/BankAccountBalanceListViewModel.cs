using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class BankAccountBalanceListViewModel(ILoggingProvider log)
    : ViewModelBase(log), IBankAccountBalanceListViewModel
{
}
