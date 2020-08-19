using System.Windows.Controls;
using AdminAssistant.UI.Shared.Breadcrumb;

namespace AdminAssistant.WPF.Shared.Breadcrumb
{
    public partial class BreadcrumbComponent : UserControl
    {
        public BreadcrumbComponent()
        {
            this.InitializeComponent();
        }

        public BreadcrumbComponent(IBreadcrumbViewModel breadcrumbViewModel)
            : this()
        {
            this.DataContext = breadcrumbViewModel;
        }
    }
}
