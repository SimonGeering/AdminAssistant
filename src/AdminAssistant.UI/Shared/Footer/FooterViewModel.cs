using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Shared.Footer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class FooterViewModel : ViewModelBase, IFooterViewModel
    {
        public FooterViewModel(ILoggingProvider log, ILoadingSpinner loadingSpinner)
            : base(log, loadingSpinner)
        {
            this.Version = $" - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string Version { get; }  
    }
}
