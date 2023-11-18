using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.MailModule;

internal sealed class MailViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IMailViewModel
{
    public string HeaderText => "Mail";
    public string SubHeaderText => string.Empty;
}
