using AdminAssistant.UI.Modules.AccountsModule;
using System.Windows.Controls;

namespace AdminAssistant.WPF.Modules.AccountsModule
{
    public partial class Accounts : UserControl
    {
        public Accounts()
        {
            this.InitializeComponent();
        }

        public Accounts(IAccountsViewModel accountsViewModel)
            : this()
        {    
            this.DataContext = accountsViewModel;
        }
    }
}
