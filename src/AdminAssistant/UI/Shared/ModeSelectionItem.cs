namespace AdminAssistant.UI.Shared;

public class ModeSelectionItem
{
    public ModeSelectionItem(ModeEnum mode, string tag, string label, string icon)
    {
        Mode = mode;
        Tag = tag;
        Label = label;
        Icon = icon;
    }

    public ModeEnum Mode { get; set; }
    public string Tag { get; set; }
    public string Label { get; set; }
    public string Icon { get; set; }

    public override string ToString() => Mode.ToString();
}
