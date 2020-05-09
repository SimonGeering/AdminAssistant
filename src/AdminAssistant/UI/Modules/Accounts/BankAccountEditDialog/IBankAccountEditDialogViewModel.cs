using System.Collections.Generic;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        BankAccount Model { get; }
        string HeaderText { get; }
        bool ShowDialog { get; set; }

        IEnumerable<BankAccountType> BankAccountTypes { get; }
        IEnumerable<Currency> Currencies { get; }

        void OnSaveButtonClick();
        void OnCancelButtonClick();

        void OnAccountNameChanged(string accountName);
        string AccountNameValidationMessage { get; }
        string AccountNameValidationClass { get; }

        void OnBankAccountTypeChanged();
        void OnCurrencyChanged();
    }
}
