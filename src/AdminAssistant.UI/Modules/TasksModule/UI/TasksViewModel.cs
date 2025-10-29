namespace AdminAssistant.Modules.TasksModule.UI;

public interface ITasksViewModel : IModuleViewModelBase;
internal sealed class TasksViewModel(ILoggingProvider loggingProvider)
    : ViewModelBase(loggingProvider), ITasksViewModel
{
    public string HeaderText => "Tasks";
    public string SubHeaderText => string.Empty;
}
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public sealed class TasksDesignerViewModel
    : DesignerViewModelBase, ITasksViewModel
{
    public string HeaderText => "Tasks (Designer)";
    public string SubHeaderText => "This is a designer view model for Tasks.";
}
