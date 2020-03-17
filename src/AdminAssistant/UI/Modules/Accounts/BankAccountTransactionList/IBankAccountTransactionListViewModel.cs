using System.Collections.Generic;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountTransactionList
{
    public interface IBankAccountTransactionListViewModel : IViewModelBase
    {
        IEnumerable<BankAccountTransaction> Transactions { get; }
    }
}
