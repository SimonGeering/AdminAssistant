using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class BankAccountBalanceListViewModel : ViewModelBase, IBankAccountBalanceListViewModel
{
    public BankAccountBalanceListViewModel(ILoggingProvider log) : base(log)
    {
    }
}
