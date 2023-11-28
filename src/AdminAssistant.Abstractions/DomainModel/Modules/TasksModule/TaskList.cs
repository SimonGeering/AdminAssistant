using AdminAssistant.Abstractions.DomainModel.Shared;

namespace AdminAssistant.DomainModel.Modules.TasksModule;

public sealed record TaskList : IDatabasePersistable
{
    public const int TaskListNameMaxLength = EntityName.MaxLength;

    public TaskListId TaskListID { get; set; } = TaskListId.Default;
    public string TaskListName { get; set; } = string.Empty;

    public Id PrimaryKey => TaskListID;
}
public sealed record TaskListId(int Value) : Id(Value)
{
    public static TaskListId Default => new(Constants.UnknownRecordID);
}
