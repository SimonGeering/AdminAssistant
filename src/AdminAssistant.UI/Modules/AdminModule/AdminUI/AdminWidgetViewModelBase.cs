namespace AdminAssistant.Modules.AdminModule.AdminUI;

public interface IAdminWidgetViewModelBase : IViewModelBase
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

internal abstract class AdminWidgetViewModelBase(ILoggingProvider log)
    : ViewModelBase(log), IAdminWidgetViewModelBase
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
