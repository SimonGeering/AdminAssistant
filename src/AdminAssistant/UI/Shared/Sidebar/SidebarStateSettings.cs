namespace AdminAssistant.UI.Shared.Sidebar
{
    public class SidebarStateSettings
    {
        public SidebarStateSettings(ExpandedContractedStateToggle state, string icon, string styleClass, bool showContent)
        {
            this.State = state;
            this.Icon = icon;
            this.StyleClass = styleClass;
            this.ShowContent = showContent;
        }

        public ExpandedContractedStateToggle State { get; private set; }
        public string Icon { get; private set; }
        public string StyleClass { get; private set; }
        public bool ShowContent { get; private set; }
    }
}
