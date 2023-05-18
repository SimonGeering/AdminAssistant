using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

public static class ModeExtensions
{
    public static PackIconFontAwesomeKind ToPackIconFontAwesomeKind(this Mode module) => module switch
    {
        Mode.Company => PackIconFontAwesomeKind.BuildingRegular,
        Mode.Personal => PackIconFontAwesomeKind.UserSolid,
        _ => PackIconFontAwesomeKind.None
    };
}
