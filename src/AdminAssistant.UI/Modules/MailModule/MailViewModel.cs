using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.MailModule;

internal class MailViewModel : ViewModelBase, IMailViewModel
{
    public MailViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Mail";

    public string SubHeaderText => string.Empty;
}
