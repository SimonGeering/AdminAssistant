namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardViewModel : IModuleViewModelBase;

internal sealed class DashboardViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDashboardViewModel
{
    public string HeaderText => "Dashboard";
    public string SubHeaderText => string.Empty;
}
