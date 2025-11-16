namespace AdminAssistant.Modules.DashboardModule.UI;

public class DashboardWidgetViewModel : IDashboardWidget
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}
