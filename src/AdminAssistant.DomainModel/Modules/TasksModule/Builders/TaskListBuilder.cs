namespace AdminAssistant.DomainModel.Modules.TasksModule.Builders;

internal sealed class TaskListBuilder : ITaskListBuilder
{
    private TaskList _taskList = new();

    public static TaskList Default(ITaskListBuilder builder) => builder.Build();
    public static TaskList Default(TaskListBuilder builder) => builder.Build();

    public TaskList Build() => _taskList;

    public ITaskListBuilder WithTestData(int taskListID = Constants.UnknownRecordID)
    {
        _taskList = _taskList with
        {
            TaskListID = taskListID,
            TaskListName = "My Task List"
        };
        return this;
    }

    public ITaskListBuilder WithTaskListName(string taskListName)
    {
        _taskList = _taskList with { TaskListName = taskListName };
        return this;
    }
}
