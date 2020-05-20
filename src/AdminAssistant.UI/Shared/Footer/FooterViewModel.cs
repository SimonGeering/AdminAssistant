using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Shared.Footer
{
    public class FooterViewModel : ViewModelBase, IFooterViewModel
    {
        public FooterViewModel(ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {
            this.Version = $" - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string Version { get; }  
    }
}
