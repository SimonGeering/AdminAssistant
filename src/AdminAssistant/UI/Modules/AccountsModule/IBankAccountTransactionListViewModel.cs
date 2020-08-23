using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList
{
    public interface IBankAccountTransactionListViewModel : IViewModelBase
    {
        BindingList<BankAccountTransaction> Transactions { get; }
    }
}
