using System;
using AdminAssistant.UI.Shared;

namespace AdminAssistant.UI.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class AppStateStore : IAppStateStore
    {
        private readonly IAppService appService;

        public AppStateStore(IAppService appService)
        {
            this.appService = appService;

            this.ActiveMode = this.appService.GetDefaultMode();
            this.ActiveModule = this.appService.GetDefaultModule();
        }

        public event Action? ActiveModeChanged;
        public event Action? ActiveModuleChanged;

        public ModeSelectionItem ActiveMode { get; private set; } 

        public ModuleSelectionItem ActiveModule { get; private set; }
        
        public void OnActiveModeChanged(ModeSelectionItem mode)
        {
            this.ActiveMode = mode;
            this.ActiveModeChanged?.Invoke();
        }

        public void OnActiveModuleChanged(ModuleSelectionItem module)
        {
            this.ActiveModule = module;
            this.ActiveModuleChanged?.Invoke();
        }
    }
}
