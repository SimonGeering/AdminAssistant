using AdminAssistant.UI.Modules.Accounts;
using System.Windows.Controls;

namespace AdminAssistant.WPF.Modules.Accounts
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
