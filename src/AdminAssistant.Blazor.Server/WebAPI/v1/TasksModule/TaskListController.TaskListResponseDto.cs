using AdminAssistant.DomainModel.Modules.TasksModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.TasksModule;

public sealed record TaskListResponseDto : IMapFrom<TaskList>
{
    public int TaskListID { get; init; }
    public string TaskListName { get; init; } = string.Empty;
}
