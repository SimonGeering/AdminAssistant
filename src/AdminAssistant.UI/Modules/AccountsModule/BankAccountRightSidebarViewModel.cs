using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
    {
        private readonly IMessenger _messenger;

        public BankAccountRightSidebarViewModel(
            ILoggingProvider log,
            IMessenger messenger)
            : base(log)
        {
            _messenger = messenger;

            AddBankAccount = new RelayCommand(execute: OnAddBankAccountButtonClick);
        }

        public IRelayCommand AddBankAccount { get; }
        
        private void OnAddBankAccountButtonClick()
        {
            Log.Start();

            _messenger.Send(new EditBankAccountMessage(new BankAccount()));
            
            Log.Finish();
        }
    }
}
