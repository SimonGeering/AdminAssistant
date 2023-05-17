using AdminAssistant.Infra.Providers;

namespace AdminAssistant.UI.Modules.TasksModule;

internal sealed class TasksViewModel : ViewModelBase, ITasksViewModel
{
    public TasksViewModel(ILoggingProvider loggingProvider)
        : base(loggingProvider)
    {
    }

    public string HeaderText => "Tasks";

    public string SubHeaderText => string.Empty;
}
