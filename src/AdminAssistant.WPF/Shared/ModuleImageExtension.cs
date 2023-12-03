using System.Windows.Markup;
using System.Windows.Media;
using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

[MarkupExtensionReturnType(typeof(ImageSource))]
public sealed class ModuleImageExtension : FontAwesomeImageExtension
{
    private Module module;

    [ConstructorArgument("module")]
    public Module Module
    {
        get => module;
        set {
            base.Kind = value.ToPackIconFontAwesomeKind();
            module = value;
        }
    }

    public ModuleImageExtension()
        : base()
    {
    }

    public ModuleImageExtension(Module module)
        : base(module.ToPackIconFontAwesomeKind())
    {
    }
}
