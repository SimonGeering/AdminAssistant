//using AdminAssistant.UI.Modules.AccountsModule.BankAccountBalanceList;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog;

namespace AdminAssistant.WPF.Modules.AccountsModule
{
    public partial class BankAccountEditDialog : Syncfusion.Windows.Shared.ChromelessWindow
    {
        public BankAccountEditDialog()
        {
            this.InitializeComponent();
        }
        public BankAccountEditDialog(IBankAccountEditDialogViewModel viewModel)
            : this()
        {
            this.DataContext = viewModel;
        }

        private void ChromelessWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as IBankAccountEditDialogViewModel).Loaded.ExecuteAsync(null);
        }
    }
}
