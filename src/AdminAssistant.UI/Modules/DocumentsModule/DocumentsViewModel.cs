using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.DocumentsModule;

internal sealed class DocumentsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDocumentsViewModel
{
    public string HeaderText => "Documents";
    public string SubHeaderText => string.Empty;
}
