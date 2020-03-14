using System.Collections.Generic;
using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Core.UI;

namespace AdminAssistant.Accounts.UI.BankAccountTransactionList
{
    public interface IBankAccountTransactionListViewModel : IViewModelBase
    {
        IEnumerable<BankAccountTransaction> Transactions { get; }
    }
    public class BankAccountTransactionListViewModel : ViewModelBase, IBankAccountTransactionListViewModel
    {
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount? SelectedBankAccount { get; }
        public IEnumerable<BankAccountTransaction> Transactions { get; } = new List<BankAccountTransaction>();
    }
}
