using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public class AccountsStateStore : PropertyChangedNotificationBase, IAccountsStateStore
    {
        public event Action<BankAccount>? CreateAccount;
        public event Action<BankAccount>? EditAccount;

        public void OnCreateAccount() => this.CreateAccount?.Invoke(new BankAccount());
        public void OnEditAccount(BankAccount bankAccount) => this.EditAccount?.Invoke(bankAccount);
    }
}
