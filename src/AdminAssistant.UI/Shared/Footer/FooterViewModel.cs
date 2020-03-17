namespace AdminAssistant.UI.Shared.Footer
{
    public class FooterViewModel : ViewModelBase, IFooterViewModel
    {
        public FooterViewModel()
        {
            this.Version = $" - V{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        public string Version { get; }  
    }
}
