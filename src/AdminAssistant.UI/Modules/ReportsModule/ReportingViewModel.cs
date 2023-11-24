using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.ReportsModule;

internal sealed class ReportsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IReportsViewModel
{
    public string HeaderText => "Reports";
    public string SubHeaderText => string.Empty;
}
