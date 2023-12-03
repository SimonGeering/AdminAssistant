namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountBalanceListViewModel : IViewModelBase;

internal sealed class BankAccountBalanceListViewModel(ILoggingProvider log)
    : ViewModelBase(log), IBankAccountBalanceListViewModel
{
}
