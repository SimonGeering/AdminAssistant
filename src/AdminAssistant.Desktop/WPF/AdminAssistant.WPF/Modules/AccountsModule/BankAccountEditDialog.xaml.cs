using AdminAssistant.UI.Modules.AccountsModule;

namespace AdminAssistant.WPF.Modules.AccountsModule
{
    public partial class BankAccountEditDialog : Syncfusion.Windows.Shared.ChromelessWindow
    {
        public BankAccountEditDialog()
        {
            this.InitializeComponent();
        }

        private void ChromelessWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) => ((IBankAccountEditDialogViewModel)this.DataContext).Loaded.ExecuteAsync(null);
    }
}
