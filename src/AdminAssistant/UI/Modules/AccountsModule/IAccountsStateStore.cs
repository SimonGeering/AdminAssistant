using System;
using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IAccountsStateStore : INotifyPropertyChanged
    {
        event Action<BankAccount>? EditAccount;
        void OnEditAccount(BankAccount bankAccount);
    }
}
