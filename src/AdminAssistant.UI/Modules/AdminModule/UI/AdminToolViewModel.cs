namespace AdminAssistant.Modules.AdminModule.UI;

public sealed class AdminToolViewModel
{
    public string Key { get; init; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
}
