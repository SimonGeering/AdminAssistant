using System.Collections.Generic;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList
{
    public class BankAccountTransactionListViewModel : ViewModelBase, IBankAccountTransactionListViewModel
    {
        public BankAccountTransactionListViewModel(ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {

        }
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount? SelectedBankAccount { get; }
        public IEnumerable<BankAccountTransaction> Transactions { get; } = new List<BankAccountTransaction>();
    }
}