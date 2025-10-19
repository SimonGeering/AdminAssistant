using AdminAssistant.Modules.TasksModule;

namespace AdminAssistant.WebAPI.v1.TasksModule;

public static class TaskModuleMapper
{
    public static IEnumerable<TaskListResponseDto> ToTaskListResponseDtoEnumeration(this IEnumerable<TaskList> source)
        => source.Select(x => new TaskListResponseDto
        {
            TaskListID = x.TaskListID.Value,
            TaskListName = x.TaskListName
        });
}
