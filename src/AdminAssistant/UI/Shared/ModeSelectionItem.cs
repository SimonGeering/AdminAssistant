namespace AdminAssistant.UI.Shared
{
    public class ModeSelectionItem
    {
        public ModeSelectionItem(ModeEnum mode, string tag, string label, string icon)
        {
            this.Mode = mode;
            this.Tag = tag;
            this.Label = label;
            this.Icon = icon;
        }

        public ModeEnum Mode { get; set; }
        public string Tag { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }

        public override string ToString() => this.Mode.ToString();
    }
}
