namespace AdminAssistant.Modules.NotesModule.UI;

public interface INotesViewModel : IModuleViewModelBase;

internal sealed class NotesViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), INotesViewModel
{
    public string HeaderText => "Notes";
    public string SubHeaderText => string.Empty;
}
[EditorBrowsable(EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class NotesDesignerViewModel
    : DesignerViewModelBase, INotesViewModel
{
    public string HeaderText => "Notes (Design Time)";
    public string SubHeaderText => string.Empty;
}
