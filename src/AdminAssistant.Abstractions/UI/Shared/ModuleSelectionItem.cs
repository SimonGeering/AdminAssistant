namespace AdminAssistant.UI.Shared;

public sealed class ModuleSelectionItem
{
    public ModuleSelectionItem(Module module, string tag, string label, string icon)
    {
        Module = module;
        Tag = tag;
        Label = label;
        Icon = icon;
        Route = $"/{module.ToString().ToLowerInvariant()}";
        StyleClass = string.Empty;
    }
    public string ID => Module.ToString();
    public Module Module { get; set; }
    public string Tag { get; set; }

    public string Label { get; set; }
    public string Icon { get; set; }
    public string Route { get; set; }
    public string StyleClass { get; set; }
}