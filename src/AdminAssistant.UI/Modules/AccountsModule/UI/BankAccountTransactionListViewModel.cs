namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountTransactionListViewModel : IViewModelBase
{
    BindingList<BankAccountTransaction> Transactions { get; }
}
internal sealed class BankAccountTransactionListViewModel(ILoggingProvider log)
    : ViewModelBase(log), IBankAccountTransactionListViewModel
{
    public bool ShowAccountEditDialog { get; }
    public BankAccount? SelectedBankAccount { get; }
    public BindingList<BankAccountTransaction> Transactions { get; } = [];
}
