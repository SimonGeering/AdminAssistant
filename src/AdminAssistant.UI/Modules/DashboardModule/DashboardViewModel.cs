using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.DashboardModule
{
    internal class DashboardViewModel : ViewModelBase, IDashboardViewModel
    {
        public DashboardViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Dashboard";

        public string SubHeaderText => string.Empty;
    }
}
