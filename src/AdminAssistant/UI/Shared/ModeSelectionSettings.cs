namespace AdminAssistant.UI.Shared.Sidebar
{
    public class ModeSelectionStateSettings
    {
        public ModeSelectionStateSettings(ExpandedContractedStateToggle state, string styleClass)
        {
            this.State = state;
            this.StyleClass = styleClass;
        }

        public ExpandedContractedStateToggle State { get; private set; }
        public string StyleClass { get; private set; }
    }
}
