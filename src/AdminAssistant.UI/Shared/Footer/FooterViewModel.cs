using AdminAssistant.Framework.Providers;

namespace AdminAssistant.UI.Shared.Footer
{
    public class FooterViewModel : ViewModelBase, IFooterViewModel
    {
        public FooterViewModel(ILoggingProvider log)
            : base(log)
        {
            this.Version = $" - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string Version { get; }  
    }
}
