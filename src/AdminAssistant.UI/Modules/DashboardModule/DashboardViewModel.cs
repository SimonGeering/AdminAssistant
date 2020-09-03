using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.DashboardModule
{
    public class DashboardViewModel : ViewModelBase, IDashboardViewModel
    {
        public DashboardViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Dashboard";

        public string SubHeaderText => string.Empty;
    }
}
