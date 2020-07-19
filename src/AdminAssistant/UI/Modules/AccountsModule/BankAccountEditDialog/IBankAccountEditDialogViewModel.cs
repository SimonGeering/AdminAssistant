using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        BankAccount Model { get; }
        string HeaderText { get; }
        bool ShowDialog { get; set; }

        IEnumerable<BankAccountType> BankAccountTypes { get; }
        IEnumerable<Currency> Currencies { get; }

        Task OnSaveButtonClick();
        void OnCancelButtonClick();

        void OnAccountNameChanged(string accountName);
        string AccountNameValidationMessage { get; }
        string AccountNameValidationClass { get; }

        void OnBankAccountTypeChanged();
        void OnCurrencyChanged();
    }
}
