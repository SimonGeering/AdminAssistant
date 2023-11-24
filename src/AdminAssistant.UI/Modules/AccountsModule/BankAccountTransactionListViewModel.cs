using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class BankAccountTransactionListViewModel(ILoggingProvider log)
    : ViewModelBase(log), IBankAccountTransactionListViewModel
{
    public bool ShowAccountEditDialog { get; }
    public BankAccount? SelectedBankAccount { get; }
    public BindingList<BankAccountTransaction> Transactions { get; } = [];
}
