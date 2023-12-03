using AdminAssistant.UI.Shared;
using MahApps.Metro.IconPacks;

namespace AdminAssistant.WPF.Shared;

public static class ModuleExtensions
{
    public static PackIconFontAwesomeKind ToPackIconFontAwesomeKind(this Module module) => module switch
    {
        Module.Accounts => PackIconFontAwesomeKind.PoundSignSolid,
        Module.AssetRegister => PackIconFontAwesomeKind.GemRegular,
        Module.Billing => PackIconFontAwesomeKind.BullseyeSolid,
        Module.Budget => PackIconFontAwesomeKind.ChartLineSolid,
        Module.Calendar => PackIconFontAwesomeKind.CalendarAltRegular,
        Module.Contacts => PackIconFontAwesomeKind.AddressBookRegular,
        Module.Dashboard => PackIconFontAwesomeKind.TachometerAltSolid,
        Module.Documents => PackIconFontAwesomeKind.FileAltRegular,
        Module.Mail => PackIconFontAwesomeKind.EnvelopeRegular,
        Module.Reports => PackIconFontAwesomeKind.ChartBarRegular,
        Module.Tasks => PackIconFontAwesomeKind.FlagRegular,
        // Module.Setup => PackIconFontAwesomeKind.ToolsSolid,
        _ => PackIconFontAwesomeKind.None
    };
}
