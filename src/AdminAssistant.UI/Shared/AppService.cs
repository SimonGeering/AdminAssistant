namespace AdminAssistant.UI.Shared;

internal sealed class AppService : IAppService
{
    private const ModeEnum DefaultMode = ModeEnum.Company;
    private const ModuleEnum DefaultModule = ModuleEnum.Dashboard;

    private readonly FontAwesomeVersionEnum _fontAwesomeVersion;

    //        private int OwnerID { get; set; } = 10; // TODO: switch to owner details later.

    public AppService(FontAwesomeVersionEnum fontAwesomeVersion)
        => _fontAwesomeVersion = fontAwesomeVersion;

    public ModeSelectionItem GetDefaultMode() => GetModeItem(DefaultMode);

    public List<ModeSelectionItem> GetModes() => new()
    {
        GetModeItem(ModeEnum.Company),
        GetModeItem(ModeEnum.Personal),
    };

    public List<ModuleSelectionItem> GetModules() => new()
    {
        GetModuleItem(ModuleEnum.Dashboard),
        GetModuleItem(ModuleEnum.Mail),
        GetModuleItem(ModuleEnum.Calendar),
        GetModuleItem(ModuleEnum.Contacts),
        GetModuleItem(ModuleEnum.Tasks),
        GetModuleItem(ModuleEnum.Notes),
        GetModuleItem(ModuleEnum.Accounts),
        GetModuleItem(ModuleEnum.AssetRegister),
        GetModuleItem(ModuleEnum.Billing),
        GetModuleItem(ModuleEnum.Budget),
        GetModuleItem(ModuleEnum.Documents),
        GetModuleItem(ModuleEnum.Reports),
        GetModuleItem(ModuleEnum.Admin)
    };

    public ModuleSelectionItem GetDefaultModule() => GetModuleItem(DefaultModule);

    private string GetLabelForMode(ModeEnum mode) => mode switch
    {
        ModeEnum.Company => "Company",
        ModeEnum.Personal => "Personal",
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private ModeSelectionItem GetModeItem(ModeEnum mode)
    {
        var label = GetLabelForMode(mode);
        return new ModeSelectionItem(mode, tag: label, label: label, icon: GetIconForMode(mode));
    }

    private ModuleSelectionItem GetModuleItem(ModuleEnum module)
    {
        var label = GetLabelForModule(module);
        return new ModuleSelectionItem(module, tag: label, label: label, icon: GetIconForModule(module));
    }

    private string GetIconForMode(ModeEnum mode) => (_fontAwesomeVersion, mode) switch
    {
        (FontAwesomeVersionEnum.V4o7o0, ModeEnum.Company) => "fa fa-building-o",
        (FontAwesomeVersionEnum.V4o7o0, ModeEnum.Personal) => "fa fa-male",

        (FontAwesomeVersionEnum.V5o15o4, ModeEnum.Company) => "far fa-building",
        (FontAwesomeVersionEnum.V5o15o4, ModeEnum.Personal) => "fas fa-male",
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    private string GetLabelForModule(ModuleEnum module) => module switch
    {
        ModuleEnum.Mail => "Mail",
        ModuleEnum.Calendar => "Calendar",
        ModuleEnum.Contacts => "Contacts",
        ModuleEnum.Tasks => "Tasks",
        ModuleEnum.Accounts => "Accounts",
        ModuleEnum.AssetRegister => "Assets",
        ModuleEnum.Billing => "Billing",
        ModuleEnum.Budget => "Budget",
        ModuleEnum.Documents => "Documents",
        ModuleEnum.Reports => "Reports",
        ModuleEnum.Dashboard => "Dashboard",
        ModuleEnum.Admin => "Admin",
        ModuleEnum.Notes => "Notes",
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };

    private string GetIconForModule(ModuleEnum module) => (_fontAwesomeVersion, module) switch
    {
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Mail) => "fa fa-envelope",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Calendar) => "fa fa-calendar",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Contacts) => "fa fa-user",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Tasks) => "fa fa-flag-o",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Notes) => "fa fa-sticky-note-o ",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Admin) => "fa fa-wrench",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Accounts) => "fa fa-gbp",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.AssetRegister) => "fa fa-diamond",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Billing) => "fa fa-bullseye",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Budget) => "fa fa-line-chart",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Documents) => "fa fa-file-text-o",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Reports) => "fa fa-bar-chart-o",
        (FontAwesomeVersionEnum.V4o7o0, ModuleEnum.Dashboard) => "fa fa-dashboard",

        // https://fontawesome.com/v5/search?o=r&m=free&f=classic
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Mail) => "far fa-envelope",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Calendar) => "fas fa-calendar-alt",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Contacts) => "fas fa-user-friends",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Tasks) => "far fa-flag",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Notes) => "far fa-sticky-note",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Admin) => "fas fa-wrench",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Accounts) => "fas fa-pound-sign",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.AssetRegister) => "far fa-gem",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Billing) => "fas fa-bullseye",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Budget) => "fas fa-chart-line",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Documents) => "far fa-file-alt",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Reports) => "fas fa-chart-bar",
        (FontAwesomeVersionEnum.V5o15o4, ModuleEnum.Dashboard) => "fas fa-tachometer-alt",
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };
}
