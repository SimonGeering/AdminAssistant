using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Services;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared.Header
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class HeaderViewModel : ViewModelBase, IHeaderViewModel
    {
        private readonly IMessenger messenger;

        public HeaderViewModel(IMessenger messenger, IAppService appService, ILoggingProvider log)
            : base(log)
        {
            this.messenger = messenger;
            this.messenger.RegisterAll(this);

            this.activeMode = appService.GetDefaultMode();
            this.activeModule = appService.GetDefaultModule();
        }

        ~HeaderViewModel() => this.messenger.UnregisterAll(this);

        private ModeSelectionItem activeMode;
        public ModeSelectionItem ActiveMode
        {
            get => activeMode;
            private set => this.SetProperty(ref activeMode, value);
        }

        private ModuleSelectionItem activeModule;
        public ModuleSelectionItem ActiveModule
        {
            get => activeModule;
            private set => this.SetProperty(ref activeModule, value);
        }

        public void Receive(ModeSelectionChangedMessage message) => this.ActiveMode = message.Value;
        public void Receive(ModuleSelectionChangedMessage message) => this.ActiveModule = message.Value;
    }
}
