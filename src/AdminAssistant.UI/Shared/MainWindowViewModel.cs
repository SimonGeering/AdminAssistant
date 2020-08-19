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

        private readonly IAccountsViewModel accountsViewModel;
        private readonly IDashboardViewModel dashboardViewModel;

        public MainWindowViewModel(IMessenger messenger, ILoggingProvider loggerProvider, IAccountsViewModel accountsViewModel, IDashboardViewModel dashboardViewModel)
            : base(loggerProvider)
        {
            this.messenger = messenger;
            this.messenger.RegisterAll(this);

            this.accountsViewModel = accountsViewModel;
            this.dashboardViewModel = dashboardViewModel;

            this.selectedViewModel = accountsViewModel;
        }
        ~MainWindowViewModel() => this.messenger.UnregisterAll(this);

        private IViewModelBase selectedViewModel;
        public IViewModelBase SelectedViewModel
        {
            get => selectedViewModel;
            private set => SetProperty(ref selectedViewModel, value);
        }

        public void Receive(ModuleSelectionChangedMessage message)
        {
            switch (message.Value.Module)
            {
                case ModuleEnum.Accounts:
                    this.SelectedViewModel = this.accountsViewModel;
                    break;

                case ModuleEnum.Dashboard:
                    this.SelectedViewModel = this.dashboardViewModel;
                    break;
            }
        }
    }
}
