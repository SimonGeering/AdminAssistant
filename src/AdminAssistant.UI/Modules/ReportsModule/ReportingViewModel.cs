using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.ReportsModule
{
    public class ReportsViewModel : ViewModelBase, IReportsViewModel
    {
        public ReportsViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Reports";

        public string SubHeaderText => string.Empty;
    }
}
