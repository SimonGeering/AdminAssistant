using AdminAssistant.WebAPI.v1.TasksModule;

namespace AdminAssistant.WebAPIClient.v1.TasksModule;

public interface ITaskListApiClient
{
    [Get("/api/v1/tasks-module/TaskList")]
    Task<IEnumerable<TaskListResponseDto>> GetTaskListsAsync(CancellationToken cancellationToken = default);
}
