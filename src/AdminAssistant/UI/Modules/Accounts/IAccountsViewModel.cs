using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts
{
    public interface IAccountsViewModel : IViewModelBase
    {
        string HeaderText { get; }
        string SubHeaderText { get; }

        BankAccount? SelectedBankAccount { get; }
    }
}
