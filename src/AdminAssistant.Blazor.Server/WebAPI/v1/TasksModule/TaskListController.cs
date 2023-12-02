using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.TasksModule.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.TasksModule;

[ApiController]
[Route("api/v1/tasks-module/[controller]")]
[ApiExplorerSettings(GroupName = "Tasks Module")]
public sealed class TaskListController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpGet]
    [SwaggerOperation("Lists all task lists.", OperationId = "GetTaskList")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of TaskListResponseDto", type: typeof(IEnumerable<TaskListResponseDto>))]
    public async Task<ActionResult<IEnumerable<TaskListResponseDto>>> GetTaskLists(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new TaskListQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<TaskListResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}

