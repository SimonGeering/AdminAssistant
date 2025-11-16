namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardWidgetViewModelBase : IViewModelBase
{
    /// <summary>
    /// Stable identifier for the widget (used for persistence, toggling, registry).
    /// </summary>
    string Key { get; init; }

    /// <summary>
    /// Display title (user-facing, can be localized).
    /// </summary>
    string Title { get; set; }

    bool IsEnabled { get; set; }
}

internal abstract class DashboardWidgetViewModelBase(ILoggingProvider log)
    : ViewModelBase(log), IDashboardWidgetViewModelBase
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
