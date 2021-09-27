namespace AdminAssistant.DomainModel.Modules.TasksModule.Builders;

public interface ITaskListBuilder
{
    TaskList Build();
    ITaskListBuilder WithTestData(int taskListID = Constants.UnknownRecordID);
    ITaskListBuilder WithTaskListName(string taskListName);
}
