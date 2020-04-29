using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        bool ShowDialog { get; set; }
        string HeaderText { get; }

        void OnSaveButtonClick();
        void OnCancelButtonClick();
    }
}
