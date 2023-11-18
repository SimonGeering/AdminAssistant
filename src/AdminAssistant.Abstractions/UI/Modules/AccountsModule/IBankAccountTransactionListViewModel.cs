using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule;

public interface IBankAccountTransactionListViewModel : IViewModelBase
{
    BindingList<BankAccountTransaction> Transactions { get; }
}
