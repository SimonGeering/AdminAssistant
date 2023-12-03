namespace AdminAssistant.Modules.DocumentsModule.UI;

public interface IDocumentsViewModel : IModuleViewModelBase;

internal sealed class DocumentsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDocumentsViewModel
{
    public string HeaderText => "Documents";
    public string SubHeaderText => string.Empty;
}
