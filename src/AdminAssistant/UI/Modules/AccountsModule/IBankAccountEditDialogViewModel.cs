using System;
using System.ComponentModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase, IRecipient<EditBankAccountMessage>
    {
        public const string NewBankAccountHeader = "New bank account";
        public const string EditBankAccountHeader = "Edit bank account";

        int BankAccountID { get; }
        int BankAccountTypeID { get; set; }
        int CurrencyID { get; set; }
        string AccountName { get; set; }
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; }
        public DateTime OpenedOn { get; set; }

        string HeaderText { get; }
        bool ShowDialog { get; set; }

        BindingList<BankAccountType> BankAccountTypes { get; }
        BindingList<Currency> Currencies { get; }

        IAsyncRelayCommand Save { get; }
        IAsyncRelayCommand Cancel { get; }

        void OnAccountNameChanged(string accountName);
        string AccountNameValidationMessage { get; }
        string AccountNameValidationClass { get; }

        void OnBankAccountTypeChanged();
        void OnCurrencyChanged();
    }
}
