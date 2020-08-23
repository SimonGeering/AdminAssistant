using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTransactionListViewModel : ViewModelBase, IBankAccountTransactionListViewModel
    {
        public BankAccountTransactionListViewModel(ILoggingProvider log)
            : base(log)
        {

        }
        public bool ShowAccountEditDialog { get; } = false;
        public BankAccount? SelectedBankAccount { get; }
        public BindingList<BankAccountTransaction> Transactions { get; } = new BindingList<BankAccountTransaction>();
    }
}
