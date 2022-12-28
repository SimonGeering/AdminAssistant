using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

public static class ModuleEnumExtensions
{
    public static PackIconFontAwesomeKind ToPackIconFontAwesomeKind(this ModuleEnum module) => module switch
    {
        ModuleEnum.Accounts => PackIconFontAwesomeKind.PoundSignSolid,
        ModuleEnum.AssetRegister => PackIconFontAwesomeKind.GemRegular,
        ModuleEnum.Billing => PackIconFontAwesomeKind.BullseyeSolid,
        ModuleEnum.Budget => PackIconFontAwesomeKind.ChartLineSolid,
        ModuleEnum.Calendar => PackIconFontAwesomeKind.CalendarAltRegular,
        ModuleEnum.Contacts => PackIconFontAwesomeKind.AddressBookRegular,
        ModuleEnum.Dashboard => PackIconFontAwesomeKind.TachometerAltSolid,
        ModuleEnum.Documents => PackIconFontAwesomeKind.FileAltRegular,
        ModuleEnum.Mail => PackIconFontAwesomeKind.EnvelopeRegular,
        ModuleEnum.Reports => PackIconFontAwesomeKind.ChartBarRegular,
        ModuleEnum.Tasks => PackIconFontAwesomeKind.FlagRegular,
        // ModuleEnum.Setup => PackIconFontAwesomeKind.ToolsSolid,
        _ => PackIconFontAwesomeKind.None
    };
}
