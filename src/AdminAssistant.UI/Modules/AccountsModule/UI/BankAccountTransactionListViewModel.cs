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
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class BankAccountTransactionListDesignerViewModel
    : DesignerViewModelBase, IBankAccountTransactionListViewModel
{
    public BindingList<BankAccountTransaction> Transactions { get; } = new BindingList<BankAccountTransaction>();
}
