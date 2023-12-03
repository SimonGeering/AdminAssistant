namespace AdminAssistant.Shared.UI;

public sealed class SidebarStateSettings(ExpandedContractedStateToggle state, string icon, string styleClass, bool showContent)
{
    public ExpandedContractedStateToggle State { get; private set; } = state;
    public string Icon { get; private set; } = icon;
    public string StyleClass { get; private set; } = styleClass;
    public bool ShowContent { get; set; } = showContent;
}
