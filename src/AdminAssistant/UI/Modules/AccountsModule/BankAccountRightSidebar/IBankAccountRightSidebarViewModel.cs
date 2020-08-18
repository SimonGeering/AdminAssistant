using Microsoft.Toolkit.Mvvm.Input;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountRightSidebar
{
    public interface IBankAccountRightSidebarViewModel : IViewModelBase
    {
        IRelayCommand AddBankAccount { get; }
    }
}
