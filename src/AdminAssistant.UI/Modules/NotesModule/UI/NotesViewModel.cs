namespace AdminAssistant.Modules.NotesModule.UI;

public interface INotesViewModel : IModuleViewModelBase;

internal sealed class NotesViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), INotesViewModel
{
    public string HeaderText => "Notes";
    public string SubHeaderText => string.Empty;
}
