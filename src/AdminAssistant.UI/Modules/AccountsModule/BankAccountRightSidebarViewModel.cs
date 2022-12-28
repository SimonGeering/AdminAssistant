using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Modules.AccountsModule;

internal sealed class BankAccountRightSidebarViewModel : ViewModelBase, IBankAccountRightSidebarViewModel
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
