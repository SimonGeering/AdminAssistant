using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.NotesModule;

internal sealed class NotesViewModel : ViewModelBase, INotesViewModel
{
    public NotesViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Notes";

    public string SubHeaderText => string.Empty;
}
