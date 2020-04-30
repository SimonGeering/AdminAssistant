using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        BankAccount BankAccount { get; }
        string HeaderText { get; }
        bool ShowDialog { get; set; }

        IEnumerable<BankAccountType> BankAccountTypes { get; }
        IEnumerable<Currency> Currencies { get; }

        void OnSaveButtonClick();
        void OnCancelButtonClick();

        Task InitializeAsync();
    }
}
