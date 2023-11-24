using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.AdminModule;

internal sealed class AdminViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IAdminViewModel
{
    public string HeaderText => "Admin";
    public string SubHeaderText => string.Empty;
}
