using AdminAssistant.UI.Modules.AccountsModule;
using System.Windows.Controls;

namespace AdminAssistant.WPF.Modules.AccountsModule
{
    public partial class AccountsComponent : UserControl
    {
        public AccountsComponent()
        {
            this.InitializeComponent();
        }

        public AccountsComponent(IAccountsViewModel accountsViewModel)
            : this()
        {    
            this.DataContext = accountsViewModel;
        }
    }
}
