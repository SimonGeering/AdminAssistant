using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AdminModule;

internal class AdminViewModel : ViewModelBase, IAdminViewModel
{
    public AdminViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Admin";

    public string SubHeaderText => string.Empty;
}
