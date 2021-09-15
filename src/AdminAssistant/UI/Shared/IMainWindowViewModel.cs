namespace AdminAssistant.UI.Shared
{
    public interface IMainWindowViewModel : IViewModelBase
    {
        SidebarStateSettings Sidebar { get; }

        List<ModeSelectionItem> Modes { get; }
        ModeSelectionItem ActiveMode { get; }

        List<ModuleSelectionItem> Modules { get; }
        ModuleSelectionItem ActiveModule { get; }
        string FooterText { get; }

        void OnSideBarControlButtonClick();

        void OnSelectedModeChanged(ModeSelectionItem mode);
        void OnSelectedModuleChanged(ModuleSelectionItem selectedModule);

    }
}
