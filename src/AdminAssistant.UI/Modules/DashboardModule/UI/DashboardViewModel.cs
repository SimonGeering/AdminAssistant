using AdminAssistant.Modules.AccountsModule.DashboardWidgetsUI;

namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardViewModel : IModuleViewModelBase
{
    IReadOnlyCollection<DashboardLayoutItemViewModel> LayoutItems { get; }
}

internal sealed class DashboardViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDashboardViewModel
{
    public string HeaderText => "Dashboard";
    public string SubHeaderText => string.Empty;

    public IReadOnlyCollection<DashboardLayoutItemViewModel> LayoutItems { get; } = new List<DashboardLayoutItemViewModel>
    {
        new DashboardLayoutItemViewModel()
        {
            Key = IAccountsSummaryDashboardWidgetViewModel.AccountsSummaryDashboardWidgetKey,
            Title = "Mock Accounts Summary",
            IsEnabled = true,
        }
    };
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class DashboardDesignerViewModel
    : DesignerViewModelBase, IDashboardViewModel
{
    public string HeaderText => "Dashboard (Demo Data)";
    public string SubHeaderText => string.Empty;

    public IReadOnlyCollection<DashboardLayoutItemViewModel> LayoutItems { get; } = new List<DashboardLayoutItemViewModel>
    {
        new DashboardLayoutItemViewModel()
        {
            Key = IAccountsSummaryDashboardWidgetViewModel.AccountsSummaryDashboardWidgetKey,
            Title = "Mock Accounts Summary",
            IsEnabled = true,
        }
    };
}
