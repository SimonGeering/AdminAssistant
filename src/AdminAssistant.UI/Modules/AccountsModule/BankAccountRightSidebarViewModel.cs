using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
    {
        private readonly IMessenger messenger;

        public BankAccountRightSidebarViewModel(
            ILoggingProvider log,
            IMessenger messenger)
            : base(log)
        {
            this.messenger = messenger;

            this.AddBankAccount = new RelayCommand(execute: this.OnAddBankAccountButtonClick);
        }

        public IRelayCommand AddBankAccount { get; }
        
        private void OnAddBankAccountButtonClick()
        {
            this.Log.Start();

            this.messenger.Send(new EditBankAccountMessage(new BankAccount()));
            
            this.Log.Finish();
        }
    }
}
