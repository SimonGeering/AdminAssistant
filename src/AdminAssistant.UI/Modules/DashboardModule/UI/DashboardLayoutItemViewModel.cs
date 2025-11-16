namespace AdminAssistant.Modules.DashboardModule.UI;

public sealed class DashboardLayoutItemViewModel
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
