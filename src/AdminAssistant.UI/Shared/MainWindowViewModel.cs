using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.DashboardModule;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace AdminAssistant.UI.Shared
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private readonly IMessenger messenger;

        public MainWindowViewModel(IMessenger messenger, ILoggingProvider loggerProvider)
            : base(loggerProvider)
        {
            this.messenger = messenger;
            this.messenger.RegisterAll(this);

            this.SelectedModule = ModuleEnum.Dashboard;
        }
        ~MainWindowViewModel() => this.messenger.UnregisterAll(this);

        private ModuleEnum selectedModule;
        public ModuleEnum SelectedModule
        {
            get => selectedModule;
            private set => SetProperty(ref selectedModule, value);
        }

        public void Receive(ModuleSelectionChangedMessage message) => this.SelectedModule = message.Value.Module;
    }
}
