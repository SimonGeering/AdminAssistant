using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IAccountsViewModel : IViewModelBase
    {
        string HeaderText { get; }
        string SubHeaderText { get; }

        BankAccount? SelectedBankAccount { get; }
    }
}
