using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.BudgetModule
{
    public class BudgetViewModel : ViewModelBase, IBudgetViewModel
    {
        public BudgetViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Budget";

        public string SubHeaderText => string.Empty;
    }
}
