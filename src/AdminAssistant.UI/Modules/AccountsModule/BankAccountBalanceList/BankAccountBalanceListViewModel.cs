using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountBalanceList
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountBalanceListViewModel : ViewModelBase, IBankAccountBalanceListViewModel
    {
        public BankAccountBalanceListViewModel(ILoggingProvider log) : base(log)
        {
        }
    }
}
