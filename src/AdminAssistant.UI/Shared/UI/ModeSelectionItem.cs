namespace AdminAssistant.Shared.UI;

public sealed class ModeSelectionItem(Mode mode, string tag, string label, string icon)
{
    public Mode Mode { get; set; } = mode;
    public string Tag { get; set; } = tag;
    public string Label { get; set; } = label;
    public string Icon { get; set; } = icon;

    public override string ToString() => Mode.ToString();
}
