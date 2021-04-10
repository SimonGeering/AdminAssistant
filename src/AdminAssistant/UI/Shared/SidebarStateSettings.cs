namespace AdminAssistant.UI.Shared
{
    public class SidebarStateSettings
    {
        public SidebarStateSettings(ExpandedContractedStateToggle state, string icon, string styleClass, bool showContent)
        {
            State = state;
            Icon = icon;
            StyleClass = styleClass;
            ShowContent = showContent;
        }

        public ExpandedContractedStateToggle State { get; private set; }
        public string Icon { get; private set; }
        public string StyleClass { get; private set; }
        public bool ShowContent { get; set; }
    }
}
