using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IAccountsStateStore
    {
        event Action<BankAccount>? EditAccount;
        void OnEditAccount(BankAccount bankAccount);
    }
}
