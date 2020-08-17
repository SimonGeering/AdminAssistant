using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Shared.Footer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class FooterViewModel : ViewModelBase, IFooterViewModel
    {
        public FooterViewModel(ILoggingProvider log) : base(log)
        {
            this.Version = $" - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string Version { get; }  
    }
}
