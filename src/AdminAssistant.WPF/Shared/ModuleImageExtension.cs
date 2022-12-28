using System.Windows.Markup;
using System.Windows.Media;
using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

[MarkupExtensionReturnType(typeof(ImageSource))]
public sealed class ModuleImageExtension : FontAwesomeImageExtension
{
    private ModuleEnum module;

    [ConstructorArgument("module")]
    public ModuleEnum Module
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

    public ModuleImageExtension(ModuleEnum module)
        : base(module.ToPackIconFontAwesomeKind())
    {
    }
}
