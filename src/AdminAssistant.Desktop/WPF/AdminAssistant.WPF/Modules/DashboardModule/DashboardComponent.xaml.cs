using System.Windows.Controls;
using AdminAssistant.UI.Modules.DashboardModule;

namespace AdminAssistant.WPF.Modules.DashboardModule
{
    public partial class DashboardComponent : UserControl
    {
        public DashboardComponent()
        {
            this.InitializeComponent();
        }

        public DashboardComponent(IDashboardViewModel dashboardViewModel)
            : this()
        {
            this.DataContext = dashboardViewModel;
        }
    }
}
