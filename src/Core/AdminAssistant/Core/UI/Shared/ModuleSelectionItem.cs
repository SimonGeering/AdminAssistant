namespace AdminAssistant.Core.UI.Shared
{
    public class ModuleSelectionItem
    {
        public ModuleSelectionItem(ModuleEnum module, string tag, string label, string icon)
        {
            this.Module = module;
            this.Tag = tag;
            this.Label = label;
            this.Icon = icon;
            this.StyleClass = string.Empty; 
        }
        public ModuleEnum Module { get; set; }
        public string Tag { get; set; }

        public string Label { get; set; }
        public string Icon { get; set; }
        public string StyleClass { get; set; }        
    }
}
