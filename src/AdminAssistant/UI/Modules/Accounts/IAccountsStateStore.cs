using System;
using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts
{
    public interface IAccountsStateStore : INotifyPropertyChanged
    {
        event Action<BankAccount>? EditAccount;
        void OnEditAccount(BankAccount bankAccount);
    }
}
