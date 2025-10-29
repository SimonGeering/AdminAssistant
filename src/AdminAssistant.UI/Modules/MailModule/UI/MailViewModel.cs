namespace AdminAssistant.Modules.MailModule.UI;

public interface IMailViewModel : IModuleViewModelBase;

internal sealed class MailViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IMailViewModel
{
    public string HeaderText => "Mail";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class MailDesignerViewModel
    : DesignerViewModelBase, IMailViewModel
{
    public string HeaderText => "Mail (Design Time)";
    public string SubHeaderText => string.Empty;
}
