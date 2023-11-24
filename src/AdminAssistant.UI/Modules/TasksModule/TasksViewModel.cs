using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.TasksModule;

internal sealed class TasksViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), ITasksViewModel
{
    public string HeaderText => "Tasks";
    public string SubHeaderText => string.Empty;
}
