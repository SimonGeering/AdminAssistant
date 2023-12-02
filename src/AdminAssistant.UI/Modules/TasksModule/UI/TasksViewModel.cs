namespace AdminAssistant.Modules.TasksModule.UI;

public interface ITasksViewModel : IModuleViewModelBase;
internal sealed class TasksViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), ITasksViewModel
{
    public string HeaderText => "Tasks";
    public string SubHeaderText => string.Empty;
}
