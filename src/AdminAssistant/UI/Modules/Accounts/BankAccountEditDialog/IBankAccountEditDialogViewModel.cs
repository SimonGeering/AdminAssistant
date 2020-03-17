using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog
{
    public interface IBankAccountEditDialogViewModel : IViewModelBase
    {
        bool ShowAccountEditDialog { get; }
        void NewAccount();
    }
}
