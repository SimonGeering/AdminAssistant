using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.NotesModule;

internal sealed class NotesViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), INotesViewModel
{
    public string HeaderText => "Notes";
    public string SubHeaderText => string.Empty;
}
