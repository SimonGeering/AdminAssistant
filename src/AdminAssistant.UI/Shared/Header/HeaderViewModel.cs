using AdminAssistant.Core.UI.Services;

namespace AdminAssistant.Core.UI.Shared.Header
{
    public class HeaderViewModel : ViewModelBase, IHeaderViewModel
    {
        private readonly IAppStateStore appStateStore;

        public HeaderViewModel(IAppStateStore appStateStore)
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
