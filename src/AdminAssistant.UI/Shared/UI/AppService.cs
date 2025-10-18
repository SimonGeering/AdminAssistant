namespace AdminAssistant.Shared.UI;

public interface IAppService
{
    ModeSelectionItem GetDefaultMode();
    ModuleSelectionItem GetDefaultModule();
    List<ModeSelectionItem> GetModes();
    List<ModuleSelectionItem> GetModules();
}

internal sealed class AppService(FontAwesomeVersion fontAwesomeVersion) : IAppService
{
    private const Mode DefaultMode = Mode.Company;
    private const Module DefaultModule = Module.Dashboard;

    public ModeSelectionItem GetDefaultMode() => GetModeItem(DefaultMode);

    public List<ModeSelectionItem> GetModes() => new()
    {
        GetModeItem(Mode.Company),
        GetModeItem(Mode.Personal),
    };

    public List<ModuleSelectionItem> GetModules() => new()
    {
        GetModuleItem(Module.Dashboard),
        GetModuleItem(Module.Mail),
        GetModuleItem(Module.Calendar),
        GetModuleItem(Module.Contacts),
        GetModuleItem(Module.Tasks),
        GetModuleItem(Module.Notes),
        GetModuleItem(Module.Accounts),
        GetModuleItem(Module.AssetRegister),
        GetModuleItem(Module.Billing),
        GetModuleItem(Module.Budget),
        GetModuleItem(Module.Documents),
        GetModuleItem(Module.Reports),
        GetModuleItem(Module.Admin)
    };

    public ModuleSelectionItem GetDefaultModule() => GetModuleItem(DefaultModule);

    private static string GetLabelForMode(Mode mode) => mode switch
    {
        Mode.Company => "Company",
        Mode.Personal => "Personal",
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private ModeSelectionItem GetModeItem(Mode mode)
    {
        var label = GetLabelForMode(mode);
        return new ModeSelectionItem(mode, tag: label, label: label, icon: GetIconForMode(mode));
    }

    private ModuleSelectionItem GetModuleItem(Module module)
    {
        var label = GetLabelForModule(module);
        return new ModuleSelectionItem(module, tag: label, label: label, icon: GetIconForModule(module));
    }

    private string GetIconForMode(Mode mode) => (fontAwesomeVersion, mode) switch
    {
        (FontAwesomeVersion.V4o7o0, Mode.Company) => "fa fa-building-o",
        (FontAwesomeVersion.V4o7o0, Mode.Personal) => "fa fa-male",

        (FontAwesomeVersion.V5o15o4, Mode.Company) => "far fa-building",
        (FontAwesomeVersion.V5o15o4, Mode.Personal) => "fas fa-male",
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private static string GetLabelForModule(Module module) => module switch
    {
        Module.Mail => "Mail",
        Module.Calendar => "Calendar",
        Module.Contacts => "Contacts",
        Module.Tasks => "Tasks",
        Module.Accounts => "Accounts",
        Module.AssetRegister => "Assets",
        Module.Billing => "Billing",
        Module.Budget => "Budget",
        Module.Documents => "Documents",
        Module.Reports => "Reports",
        Module.Dashboard => "Dashboard",
        Module.Admin => "Admin",
        Module.Notes => "Notes",
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };

    private string GetIconForModule(Module module) => (fontAwesomeVersion, module) switch
    {
        (FontAwesomeVersion.V4o7o0, Module.Mail) => "fa fa-envelope",
        (FontAwesomeVersion.V4o7o0, Module.Calendar) => "fa fa-calendar",
        (FontAwesomeVersion.V4o7o0, Module.Contacts) => "fa fa-user",
        (FontAwesomeVersion.V4o7o0, Module.Tasks) => "fa fa-flag-o",
        (FontAwesomeVersion.V4o7o0, Module.Notes) => "fa fa-sticky-note-o ",
        (FontAwesomeVersion.V4o7o0, Module.Admin) => "fa fa-wrench",
        (FontAwesomeVersion.V4o7o0, Module.Accounts) => "fa fa-gbp",
        (FontAwesomeVersion.V4o7o0, Module.AssetRegister) => "fa fa-diamond",
        (FontAwesomeVersion.V4o7o0, Module.Billing) => "fa fa-bullseye",
        (FontAwesomeVersion.V4o7o0, Module.Budget) => "fa fa-line-chart",
        (FontAwesomeVersion.V4o7o0, Module.Documents) => "fa fa-file-text-o",
        (FontAwesomeVersion.V4o7o0, Module.Reports) => "fa fa-bar-chart-o",
        (FontAwesomeVersion.V4o7o0, Module.Dashboard) => "fa fa-dashboard",

        // https://fontawesome.com/v5/search?o=r&m=free&f=classic
        (FontAwesomeVersion.V5o15o4, Module.Mail) => "far fa-envelope",
        (FontAwesomeVersion.V5o15o4, Module.Calendar) => "fas fa-calendar-alt",
        (FontAwesomeVersion.V5o15o4, Module.Contacts) => "fas fa-user-friends",
        (FontAwesomeVersion.V5o15o4, Module.Tasks) => "far fa-flag",
        (FontAwesomeVersion.V5o15o4, Module.Notes) => "far fa-sticky-note",
        (FontAwesomeVersion.V5o15o4, Module.Admin) => "fas fa-wrench",
        (FontAwesomeVersion.V5o15o4, Module.Accounts) => "fas fa-pound-sign",
        (FontAwesomeVersion.V5o15o4, Module.AssetRegister) => "far fa-gem",
        (FontAwesomeVersion.V5o15o4, Module.Billing) => "fas fa-bullseye",
        (FontAwesomeVersion.V5o15o4, Module.Budget) => "fas fa-chart-line",
        (FontAwesomeVersion.V5o15o4, Module.Documents) => "far fa-file-alt",
        (FontAwesomeVersion.V5o15o4, Module.Reports) => "fas fa-chart-bar",
        (FontAwesomeVersion.V5o15o4, Module.Dashboard) => "fas fa-tachometer-alt",
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };
}
