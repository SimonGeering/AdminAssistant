namespace AdminAssistant.Modules.AccountsModule.UI;

public interface IBankAccountRightSidebarViewModel : IViewModelBase
{
    IRelayCommand AddBankAccount { get; }
}
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
