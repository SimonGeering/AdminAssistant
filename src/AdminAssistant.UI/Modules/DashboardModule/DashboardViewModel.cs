using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.DashboardModule;

internal sealed class DashboardViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDashboardViewModel
{
    public string HeaderText => "Dashboard";
    public string SubHeaderText => string.Empty;
}
