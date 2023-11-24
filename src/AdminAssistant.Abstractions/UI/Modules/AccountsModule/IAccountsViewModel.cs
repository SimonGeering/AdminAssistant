using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule;

public interface IAccountsViewModel : IModuleViewModelBase
{
    BankAccount? SelectedBankAccount { get; }
}
