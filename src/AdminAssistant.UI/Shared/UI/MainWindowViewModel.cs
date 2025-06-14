namespace AdminAssistant.Shared.UI;

public interface IMainWindowViewModel : IViewModelBase
{
    SidebarStateSettings Sidebar { get; }

    List<ModeSelectionItem> Modes { get; }
    ModeSelectionItem ActiveMode { get; }

    List<ModuleSelectionItem> Modules { get; }
    ModuleSelectionItem ActiveModule { get; }
    string FooterText { get; }

    void OnSideBarControlButtonClick();

    void OnSelectedModeChanged(ModeSelectionItem selectedMode);
    void OnSelectedModuleChanged(ModuleSelectionItem selectedModule);
}
internal sealed class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
{
    private const string SelectedModuleStyle = "active";

    private readonly IMessenger _messenger;

    private readonly SidebarStateSettings _contractedSidebarState;
    private readonly SidebarStateSettings _expandedSidebarState;

#if DEBUG // Design Time implementation
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    internal MainWindowViewModel() : base(null!)
        => FooterText = $"[Designer Mock] - Admin Assistant - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#endif // DEBUG - Design Time implementation

    public MainWindowViewModel(IMessenger messenger, IAppService appService, ILoggingProvider loggerProvider)
        : base(loggerProvider)
    {
        Log.Start();

        _messenger = messenger;
        _messenger.RegisterAll(this);

        _activeMode = appService.GetDefaultMode();
        _activeModule = appService.GetDefaultModule();

        FooterText = $"Admin Assistant - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

        _contractedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Contracted, "fa fa-lg fa-angle-double-right", "cl-navbar-contracted", false);
        _expandedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Expanded, "fa fa-lg fa-angle-double-left", "cl-navbar-expanded", true);
        Sidebar = _expandedSidebarState;

        Modes = appService.GetModes();
        ActiveMode = appService.GetDefaultMode();

        Modules = appService.GetModules();
        ActiveModule = appService.GetDefaultModule();
        ActiveModule.StyleClass = SelectedModuleStyle;

        Log.Finish();
    }
    ~MainWindowViewModel() => _messenger.UnregisterAll(this);

    private ModeSelectionItem _activeMode;
    public ModeSelectionItem ActiveMode
    {
        get => _activeMode;
        set => SetProperty(ref _activeMode, value);
    }

    private ModuleSelectionItem _activeModule;
    public ModuleSelectionItem ActiveModule
    {
        get => _activeModule;
        set => SetProperty(ref _activeModule, value);
    }

    public SidebarStateSettings Sidebar { get; private set; }

    public List<ModeSelectionItem> Modes { get; private set; }

    public List<ModuleSelectionItem> Modules { get; private set; }

    public void OnSideBarControlButtonClick()
    {
        switch (Sidebar.State)
        {
            case ExpandedContractedStateToggle.Contracted:
                Sidebar = _expandedSidebarState;
                Modes.ForEach((module) => module.Label = module.Tag);
                Modules.ForEach((module) => module.Label = module.Tag);
                break;

            case ExpandedContractedStateToggle.Expanded:
                Sidebar = _contractedSidebarState;
                Modes.ForEach((module) => module.Label = string.Empty);
                Modules.ForEach((module) => module.Label = string.Empty);
                break;
        }
        OnPropertyChanged(nameof(Sidebar));
    }

    public void OnSelectedModeChanged(ModeSelectionItem selectedMode)
    {
        ActiveMode = selectedMode;
        _messenger.Send(new ModeSelectionChangedMessage(ActiveMode));
    }

    public void OnSelectedModuleChanged(ModuleSelectionItem selectedModule)
    {
        ActiveModule = selectedModule;
        Modules.ForEach(x => x.StyleClass = string.Empty);
        ActiveModule.StyleClass = SelectedModuleStyle;

        _messenger.Send(new ModuleSelectionChangedMessage(ActiveModule));
    }

    public static string PageTitle => "Admin Assistant";
    public string FooterText { get; }
}
