namespace AdminAssistant.WebAPI.v1.TasksModule;

public sealed record TaskListResponseDto
{
    public int TaskListID { get; init; }
    public string TaskListName { get; init; } = string.Empty;
}
