using System.Windows;
using AdminAssistant.UI.Modules.AccountsModule;
using Syncfusion.Windows.Shared;

namespace AdminAssistant.WPF.Modules.AccountsModule
{
    public partial class BankAccountEditDialog : ChromelessWindow
    {
        public BankAccountEditDialog() => InitializeComponent();

        private void ChromelessWindow_Loaded(object sender, RoutedEventArgs e) => ((IBankAccountEditDialogViewModel)DataContext).Loaded.ExecuteAsync(null);
    }
}
