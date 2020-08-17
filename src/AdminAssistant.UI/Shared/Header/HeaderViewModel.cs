using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Services;

namespace AdminAssistant.UI.Shared.Header
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class HeaderViewModel : ViewModelBase, IHeaderViewModel
    {
        private readonly IAppStateStore appStateStore;

        public HeaderViewModel(IAppStateStore appStateStore, ILoggingProvider log) : base(log)
        {
            this.appStateStore = appStateStore;
            
            this.ActiveMode = this.appStateStore.ActiveMode;
            this.ActiveModule = this.appStateStore.ActiveModule;

            this.appStateStore.ActiveModeChanged += () => 
            { 
                this.ActiveMode = this.appStateStore.ActiveMode; 
                this.OnPropertyChanged(); 
            };
            this.appStateStore.ActiveModuleChanged += () => 
            { 
                this.ActiveModule = this.appStateStore.ActiveModule;
                this.OnPropertyChanged();
            };
        }

        public ModeSelectionItem ActiveMode { get; private set; }

        public ModuleSelectionItem ActiveModule { get; private set; }
    }
}
