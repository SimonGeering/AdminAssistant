using System.Windows.Controls;
using AdminAssistant.UI.Modules.AdminModule;

namespace AdminAssistant.WPF.Modules.Admin
{
    public partial class AdminComponent : UserControl
    {
        public AdminComponent()
        {
            this.InitializeComponent();
        }

        public AdminComponent(IAdminViewModel adminViewModel)
            : this()
        {
            this.DataContext = adminViewModel;
        }
    }
}
