namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardViewModel : IModuleViewModelBase;

internal sealed class DashboardViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDashboardViewModel
{
    public string HeaderText => "Dashboard";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class DashboardDesignerViewModel
    : DesignerViewModelBase, IDashboardViewModel
{
    public string HeaderText => "Dashboard (Design Time)";
    public string SubHeaderText => string.Empty;
}
