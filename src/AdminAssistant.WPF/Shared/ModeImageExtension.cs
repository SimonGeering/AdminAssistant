using System.Windows.Markup;
using System.Windows.Media;
using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

[MarkupExtensionReturnType(typeof(ImageSource))]
public sealed class ModeImageExtension : FontAwesomeImageExtension
{
    private Mode mode;

    [ConstructorArgument("mode")]
    public Mode Mode
    {
        get => mode;
        set
        {
            base.Kind = value.ToPackIconFontAwesomeKind();
            mode = value;
        }
    }

    public ModeImageExtension()
        : base()
    {
    }

    public ModeImageExtension(Mode mode)
        : base(mode.ToPackIconFontAwesomeKind())
    {
    }
}
