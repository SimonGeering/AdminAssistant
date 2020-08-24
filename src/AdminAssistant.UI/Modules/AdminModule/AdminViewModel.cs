using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Modules.AdminModule
{
    public class AdminViewModel : ViewModelBase, IAdminViewModel
    {
        public AdminViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Admin";

        public string SubHeaderText => string.Empty;
    }
}
