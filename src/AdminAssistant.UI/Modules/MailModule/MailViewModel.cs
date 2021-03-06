using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.MailModule
{
    public class MailViewModel : ViewModelBase, IMailViewModel
    {
        public MailViewModel(ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
        }

        public string HeaderText => "Mail";

        public string SubHeaderText => string.Empty;
    }
}
