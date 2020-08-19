using System.Windows.Controls;
using AdminAssistant.UI.Modules.BillingModule;

namespace AdminAssistant.WPF.Modules.BillingModule
{
    public partial class BillingComponent : UserControl
    {
        public BillingComponent()
        {
            this.InitializeComponent();
        }

        public BillingComponent(IBillingViewModel billingViewModel)
            : this()
        {
            this.DataContext = billingViewModel;
        }
    }
}
