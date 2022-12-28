using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.DocumentsModule;

internal sealed class DocumentsViewModel : ViewModelBase, IDocumentsViewModel
{
    public DocumentsViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Documents";

    public string SubHeaderText => string.Empty;
}
