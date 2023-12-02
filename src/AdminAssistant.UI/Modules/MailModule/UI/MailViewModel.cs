namespace AdminAssistant.Modules.MailModule.UI;

public interface IMailViewModel : IModuleViewModelBase;

internal sealed class MailViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IMailViewModel
{
    public string HeaderText => "Mail";
    public string SubHeaderText => string.Empty;
}
