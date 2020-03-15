using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Core.UI;

namespace AdminAssistant.Accounts.UI
{
    public interface IAccountsViewModel : IViewModelBase
    {
        string HeaderText { get; }
        string SubHeaderText { get; }

        BankAccount? SelectedBankAccount { get; }
    }
    public class AccountsViewModel : ViewModelBase, IAccountsViewModel
    {
        public string HeaderText { get; } = "Accounts";
        public string SubHeaderText { get; } = string.Empty;

        public BankAccount? SelectedBankAccount { get; }
    }
}
