namespace AdminAssistant.DomainModel.Modules.TasksModule.Builders
{
    public interface ITaskListBuilder
    {
        TaskList Build();
        ITaskListBuilder WithTestData(int taskListID = Constants.UnknownRecordID);
        ITaskListBuilder WithTaskListName(string taskListName);
    }
    internal class TaskListBuilder : TaskList, ITaskListBuilder
    {
        public static TaskList Default(ITaskListBuilder builder) => builder.Build();
        public static TaskList Default(TaskListBuilder builder) => builder.Build();

        public TaskList Build() => this;

        public ITaskListBuilder WithTestData(int taskListID = Constants.UnknownRecordID)
        {
            TaskListID = taskListID;
            TaskListName = "My Task List";
            return this;
        }
        public ITaskListBuilder WithTaskListName(string taskListName)
        {
            TaskListName = taskListName;
            return this;
        }
    }
}
