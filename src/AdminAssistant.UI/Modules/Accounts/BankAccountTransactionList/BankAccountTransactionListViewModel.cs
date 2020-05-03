using System.Collections.Generic;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountTransactionList
{
    public class BankAccountTransactionListViewModel : ViewModelBase, IBankAccountTransactionListViewModel
    {
        public BankAccountTransactionListViewModel(ILoggingProvider log)
            : base(log)
        {

        }
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount? SelectedBankAccount { get; }
        public IEnumerable<BankAccountTransaction> Transactions { get; } = new List<BankAccountTransaction>();
    }
}
