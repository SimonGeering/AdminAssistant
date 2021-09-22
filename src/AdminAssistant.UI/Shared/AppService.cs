namespace AdminAssistant.UI.Shared;

internal class AppService : IAppService
{
    private const ModeEnum DefaultMode = ModeEnum.Company;
    private const ModuleEnum DefaultModule = ModuleEnum.Dashboard;

    //        private int OwnerID { get; set; } = 10; // TODO: switch to owner details later.
    //
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
        GetModuleItem(ModuleEnum.Accounts),
        GetModuleItem(ModuleEnum.AssetRegister),
        GetModuleItem(ModuleEnum.Billing),
        GetModuleItem(ModuleEnum.Budget),
        GetModuleItem(ModuleEnum.Documents),
        GetModuleItem(ModuleEnum.Reports),
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

    private string GetIconForMode(ModeEnum mode) => mode switch
    {
        ModeEnum.Company => "fa fa-building-o",
        ModeEnum.Personal => "fa fa-male",
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
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };

    private string GetIconForModule(ModuleEnum module) => module switch
    {
        ModuleEnum.Mail => "fa fa-envelope",
        ModuleEnum.Calendar => "fa fa-calendar",
        ModuleEnum.Contacts => "fa fa-user",
        ModuleEnum.Tasks => "fa fa-flag-o",
        ModuleEnum.Accounts => "fa fa-gbp",
        ModuleEnum.AssetRegister => "fa fa-diamond",
        ModuleEnum.Billing => "fa fa-bullseye",
        ModuleEnum.Budget => "fa fa-line-chart",
        ModuleEnum.Documents => "fa fa-file-text-o",
        ModuleEnum.Reports => "fa fa-bar-chart-o",
        ModuleEnum.Dashboard => "fa fa-dashboard",
        _ => throw new ArgumentOutOfRangeException(nameof(module))
    };
}
