using System.Collections.Generic;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Services;

namespace AdminAssistant.UI.Shared.Sidebar
{
    public class SidebarViewModel : ViewModelBase, ISidebarViewModel
    {
        private const string SelectedModuleStyle = "active";

        private readonly IAppService appService;
        private readonly IAppStateStore appStateStore;

        private readonly SidebarStateSettings contractedSidebarState;
        private readonly SidebarStateSettings expandedSidebarState;
        private readonly ModeSelectionStateSettings contractedModeSelectionDropDownState;
        private readonly ModeSelectionStateSettings expandedModeSelectionDropDownState;

        public SidebarViewModel(IAppStateStore appStateStore, IAppService appService, ILoggingProvider log)
            : base(log)
        {
            this.appStateStore = appStateStore;
            this.appService = appService;

            this.contractedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Contracted, "fa fa-lg fa-angle-double-right", "cl-navbar-contracted", false);
            this.expandedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Expanded, "fa fa-lg fa-angle-double-left", "cl-navbar-expanded", true);
            this.Sidebar = this.expandedSidebarState;

            this.contractedModeSelectionDropDownState = new ModeSelectionStateSettings(ExpandedContractedStateToggle.Contracted, string.Empty);
            this.expandedModeSelectionDropDownState = new ModeSelectionStateSettings(ExpandedContractedStateToggle.Expanded, "show");
            this.ModeSelectionDropDown = this.contractedModeSelectionDropDownState;

            this.Modes = this.appService.GetModes();
            this.ActiveMode = this.appService.GetDefaultMode();
            
            this.Modules = this.appService.GetModules();
            this.ActiveModule = this.appService.GetDefaultModule();
            this.ActiveModule.StyleClass = SelectedModuleStyle;
        }

        public SidebarStateSettings Sidebar { get; private set; }
        public ModeSelectionStateSettings ModeSelectionDropDown { get; private set; }

        public List<ModeSelectionItem> Modes { get; private set; }
        public ModeSelectionItem ActiveMode { get; private set; }

        public List<ModuleSelectionItem> Modules { get; private set; }
        public ModuleSelectionItem ActiveModule { get; private set; }

        public void OnModeSelectionDropDownClick()
        {
            switch (this.ModeSelectionDropDown.State)
            {
                case ExpandedContractedStateToggle.Contracted:
                    this.ModeSelectionDropDown = this.expandedModeSelectionDropDownState;
                    break;

                case ExpandedContractedStateToggle.Expanded:
                    this.ModeSelectionDropDown = this.contractedModeSelectionDropDownState;
                    break;
            }
        }

        public void OnSideBarControlButtonClick()
        {
            switch (this.Sidebar.State)
            {
                case ExpandedContractedStateToggle.Contracted:
                    this.Sidebar = this.expandedSidebarState;
                    this.Modes.ForEach((module) => { module.Label = module.Tag; });
                    this.Modules.ForEach((module) => { module.Label = module.Tag; });
                    break;

                case ExpandedContractedStateToggle.Expanded:
                    this.Sidebar = this.contractedSidebarState;
                    this.Modes.ForEach((module) => { module.Label = string.Empty; });
                    this.Modules.ForEach((module) => { module.Label = string.Empty; });
                    break;
            }
            this.OnPropertyChanged(nameof(this.Sidebar));
        }

        public void OnSelectedModeChanged(ModeSelectionItem selectedMode)
        {
            this.ActiveMode = selectedMode;
            this.ModeSelectionDropDown = this.contractedModeSelectionDropDownState;

            this.appStateStore.OnActiveModeChanged(this.ActiveMode);
        }

        public void OnSelectedModuleChanged(ModuleSelectionItem selectedModule)
        {
            this.ActiveModule = selectedModule;
            this.Modules.ForEach(x => x.StyleClass = string.Empty);
            this.ActiveModule.StyleClass = SelectedModuleStyle;

            this.appStateStore.OnActiveModuleChanged(this.ActiveModule);
        }
    }
}
