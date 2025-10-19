using AdminAssistant.Modules.TasksModule.Queries;

namespace AdminAssistant.WebAPI.v1.TasksModule;

[ApiController]
[Route("api/v1/tasks-module/[controller]")]
[ApiExplorerSettings(GroupName = "Tasks Module")]
public sealed class TaskListController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all task lists.", OperationId = "GetTaskList")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of TaskListResponseDto", type: typeof(IEnumerable<TaskListResponseDto>))]
    public async Task<ActionResult<IEnumerable<TaskListResponseDto>>> GetTaskLists(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new TaskListQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToTaskListResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}

