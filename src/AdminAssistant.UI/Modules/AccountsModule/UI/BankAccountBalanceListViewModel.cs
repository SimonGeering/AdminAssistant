namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountBalanceListViewModel : IViewModelBase;

internal sealed class BankAccountBalanceListViewModel(ILoggingProvider log)
    : ViewModelBase(log), IBankAccountBalanceListViewModel
{
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class BankAccountBalanceListDesignerViewModel
    : DesignerViewModelBase, IBankAccountBalanceListViewModel
{
}
