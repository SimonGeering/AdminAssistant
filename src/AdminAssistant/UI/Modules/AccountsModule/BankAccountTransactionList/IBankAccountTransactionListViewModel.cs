using System.Collections.Generic;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList
{
    public interface IBankAccountTransactionListViewModel : IViewModelBase
    {
        IEnumerable<BankAccountTransaction> Transactions { get; }
    }
}
