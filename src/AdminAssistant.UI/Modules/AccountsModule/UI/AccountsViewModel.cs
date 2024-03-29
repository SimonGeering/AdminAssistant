namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IAccountsViewModel : IModuleViewModelBase
{
    BankAccount? SelectedBankAccount { get; }
}
internal sealed class AccountsViewModel(ILoggingProvider log)
    : ViewModelBase(log), IAccountsViewModel
{
    public string HeaderText { get; } = "Accounts";
    public string SubHeaderText { get; } = string.Empty;

    public BankAccount? SelectedBankAccount { get; }
}
