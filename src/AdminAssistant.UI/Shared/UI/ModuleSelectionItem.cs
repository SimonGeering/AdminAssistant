namespace AdminAssistant.Shared.UI;

public sealed class ModuleSelectionItem(Module module, string tag, string label, string icon)
{
    public string ID => Module.ToString();
    public Module Module { get; set; } = module;
    public string Tag { get; set; } = tag;

    public string Label { get; set; } = label;
    public string Icon { get; set; } = icon;
    public string Route { get; set; } = $"/{module.ToString().ToLowerInvariant()}";
    public string StyleClass { get; set; } = string.Empty;
}
