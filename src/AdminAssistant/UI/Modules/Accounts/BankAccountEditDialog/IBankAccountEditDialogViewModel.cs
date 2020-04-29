using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        BankAccount BankAccount { get; }
        string HeaderText { get; }
        bool ShowDialog { get; set; }

        void OnSaveButtonClick();
        void OnCancelButtonClick();
    }
}
