using System.Windows.Controls;
using AdminAssistant.UI.Modules.ReportsModule;

namespace AdminAssistant.WPF.Modules.ReportsModule
{
    public partial class ReportsComponent : UserControl
    {
        public ReportsComponent() => InitializeComponent();

        public ReportsComponent(IReportsViewModel reportsViewModel)
            : this() => DataContext = reportsViewModel;
    }
}
