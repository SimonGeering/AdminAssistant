using System.Windows.Controls;
using AdminAssistant.UI.Modules.BudgetModule;

namespace AdminAssistant.WPF.Modules.BudgetModule
{
    public partial class BudgetComponent : UserControl
    {
        public BudgetComponent()
        {
            this.InitializeComponent();
        }

        public BudgetComponent(IBudgetViewModel budgetViewModel)
            : this()
        {
            this.DataContext = budgetViewModel;
        }
    }
}
