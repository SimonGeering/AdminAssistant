using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class AccountsStateStore : PropertyChangedNotificationBase, IAccountsStateStore
    {
        public event Action<BankAccount>? CreateAccount;
        public event Action<BankAccount>? EditAccount;

        public void OnCreateAccount() => this.CreateAccount?.Invoke(new BankAccount());
        public void OnEditAccount(BankAccount bankAccount) => this.EditAccount?.Invoke(bankAccount);
    }
}
