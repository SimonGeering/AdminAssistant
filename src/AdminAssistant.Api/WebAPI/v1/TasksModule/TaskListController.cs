using AdminAssistant.Modules.TasksModule.Queries;

namespace AdminAssistant.WebAPI.v1.TasksModule;

[ApiController]
[Route("api/v1/tasks-module/[controller]")]
[ApiExplorerSettings(GroupName = "Tasks Module")]
public sealed class TaskListController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all task lists.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="TaskListResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskListResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskListResponseDto>>> GetTaskLists(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new TaskListQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToTaskListResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
