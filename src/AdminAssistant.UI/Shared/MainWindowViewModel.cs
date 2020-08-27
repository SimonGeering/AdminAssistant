using System.Collections.Generic;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Services;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private const string SelectedModuleStyle = "active";

        private readonly IMessenger messenger;
        private readonly IAppService appService;

        private readonly SidebarStateSettings contractedSidebarState;
        private readonly SidebarStateSettings expandedSidebarState;

        public MainWindowViewModel(IMessenger messenger, IAppService appService, ILoggingProvider loggerProvider)
            : base(loggerProvider)
        {
            this.Log.Start();

            this.messenger = messenger;
            this.messenger.RegisterAll(this);

            this.appService = appService;

            this.activeMode = appService.GetDefaultMode();
            this.activeModule = appService.GetDefaultModule();

            this.FooterText = $"Admin Assistant - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";

            this.contractedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Contracted, "fa fa-lg fa-angle-double-right", "cl-navbar-contracted", false);
            this.expandedSidebarState = new SidebarStateSettings(ExpandedContractedStateToggle.Expanded, "fa fa-lg fa-angle-double-left", "cl-navbar-expanded", true);
            this.Sidebar = this.expandedSidebarState;

            this.Modes = this.appService.GetModes();
            this.ActiveMode = this.appService.GetDefaultMode();

            this.Modules = this.appService.GetModules();
            this.ActiveModule = this.appService.GetDefaultModule();
            this.ActiveModule.StyleClass = SelectedModuleStyle;

            this.Log.Finish();
        }
        ~MainWindowViewModel() => this.messenger.UnregisterAll(this);

        private ModeSelectionItem activeMode;
        public ModeSelectionItem ActiveMode
        {
            get => this.activeMode;
            set => this.SetProperty(ref this.activeMode, value);
        }

        private ModuleSelectionItem activeModule;
        public ModuleSelectionItem ActiveModule
        {
            get => this.activeModule;
            set => this.SetProperty(ref this.activeModule, value);
        }

        public SidebarStateSettings Sidebar { get; private set; }

        public List<ModeSelectionItem> Modes { get; private set; }

        public List<ModuleSelectionItem> Modules { get; private set; }

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
            //this.ModeSelectionDropDown = this.contractedModeSelectionDropDownState;

            this.messenger.Send(new ModeSelectionChangedMessage(this.ActiveMode));
        }

        public void OnSelectedModuleChanged(ModuleSelectionItem selectedModule)
        {
            this.ActiveModule = selectedModule;
            this.Modules.ForEach(x => x.StyleClass = string.Empty);
            this.ActiveModule.StyleClass = SelectedModuleStyle;

            this.messenger.Send(new ModuleSelectionChangedMessage(this.ActiveModule));
        }

        public string FooterText { get; }
    }
}
