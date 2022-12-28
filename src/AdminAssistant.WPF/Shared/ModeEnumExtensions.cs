using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

public static class ModeEnumExtensions
{
    public static PackIconFontAwesomeKind ToPackIconFontAwesomeKind(this ModeEnum module) => module switch
    {
        ModeEnum.Company => PackIconFontAwesomeKind.BuildingRegular,
        ModeEnum.Personal => PackIconFontAwesomeKind.UserSolid,
        _ => PackIconFontAwesomeKind.None
    };
}
