namespace AdminAssistant.Modules.DashboardModule.UI;

public interface IDashboardWidget
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

    /// <summary>
    /// Arbitrary parameters passed to the component.
    /// </summary>
    Dictionary<string, object> Parameters { get; set; }
}
