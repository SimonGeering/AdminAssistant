namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardViewModel : IModuleViewModelBase
{
    IReadOnlyCollection<IDashboardWidget> Widgets { get; }
}

internal sealed class DashboardViewModel
    : ViewModelBase, IDashboardViewModel
{
    public string HeaderText => "Dashboard";
    public string SubHeaderText => string.Empty;
    public IReadOnlyCollection<IDashboardWidget> Widgets { get; } = Array.Empty<IDashboardWidget>();

    public DashboardViewModel(
        ILoggingProvider loggingProvider,
        IEnumerable<IWidgetProvider> providers)
        : base(loggingProvider)
    {
        // Initialize the Widgets collection using the provided widget providers
        Widgets = providers.SelectMany(p => p.GetWidgets()).ToArray();
    }
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class DashboardDesignerViewModel
    : DesignerViewModelBase, IDashboardViewModel
{
    public string HeaderText => "Dashboard (Demo Data)";
    public string SubHeaderText => string.Empty;

    public IReadOnlyCollection<IDashboardWidget> Widgets { get; } = new List<IDashboardWidget>
    {
        new DashboardWidgetViewModel
        {
            Key = "Demo.Accounts",
            Title = "Mock Accounts Summary",
            IsEnabled = true,
            Parameters = new Dictionary<string, object>()
        },
        new DashboardWidgetViewModel
        {
            Key = "Demo.Tasks",
            Title = "Mock Task List",
            IsEnabled = true,
            Parameters = new Dictionary<string, object>()
        }
    };
}
