using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
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

        Task OnSaveButtonClick();
        void OnCancelButtonClick();

        void OnAccountNameChanged(string accountName);
        string AccountNameValidationMessage { get; }
        string AccountNameValidationClass { get; }

        void OnBankAccountTypeChanged();
        void OnCurrencyChanged();
    }
}
