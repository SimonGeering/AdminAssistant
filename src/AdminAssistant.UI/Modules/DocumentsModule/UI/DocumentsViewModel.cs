namespace AdminAssistant.Modules.DocumentsModule.UI;

public interface IDocumentsViewModel : IModuleViewModelBase;

internal sealed class DocumentsViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), IDocumentsViewModel
{
    public string HeaderText => "Documents";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class DocumentsDesignerViewModel
    : DesignerViewModelBase, IDocumentsViewModel
{
    public string HeaderText => "Documents (Design Time)";
    public string SubHeaderText => string.Empty;
}
