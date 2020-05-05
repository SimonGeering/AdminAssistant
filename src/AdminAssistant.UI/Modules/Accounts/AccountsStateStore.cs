using System;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts
{
    public class AccountsStateStore : PropertyChangedNotificationBase, IAccountsStateStore
    {
        public event Action<BankAccount>? CreateAccount;
        public event Action<BankAccount>? EditAccount;

        public void OnCreateAccount() => this.CreateAccount?.Invoke(new BankAccount());
        public void OnEditAccount(BankAccount bankAccount) => this.EditAccount?.Invoke(bankAccount);
    }
}