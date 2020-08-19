using System.Windows.Controls;
using AdminAssistant.UI.Shared.Sidebar;

namespace AdminAssistant.WPF.Shared.Sidebar
{
    public partial class SidebarComponent : UserControl
    {
        public SidebarComponent()
        {
            this.InitializeComponent();
        }
        public SidebarComponent(ISidebarViewModel sidebarViewModel)
            : this()
        {
            this.DataContext = sidebarViewModel;
        }
    }
}
