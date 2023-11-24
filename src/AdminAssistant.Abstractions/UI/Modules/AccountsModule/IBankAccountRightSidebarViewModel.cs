using Microsoft.Toolkit.Mvvm.Input;

namespace AdminAssistant.UI.Modules.AccountsModule;

public interface IBankAccountRightSidebarViewModel : IViewModelBase
{
    IRelayCommand AddBankAccount { get; }
}
