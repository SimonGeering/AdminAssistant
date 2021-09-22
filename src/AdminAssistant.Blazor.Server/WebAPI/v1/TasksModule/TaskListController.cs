using AdminAssistant.DomainModel.Modules.TasksModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.TasksModule;

[ApiController]
[Route("api/v1/tasks-module/[controller]")]
[ApiExplorerSettings(GroupName = "Tasks Module")]
public class TaskListController : WebAPIControllerBase
{
    public TaskListController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
        : base(mapper, mediator, loggingProvider)
    {
    }

    [HttpGet]
    [SwaggerOperation("Lists all task lists.", OperationId = "GetTaskList")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of TaskListResponseDto", type: typeof(IEnumerable<TaskListResponseDto>))]
    public async Task<ActionResult<IEnumerable<TaskListResponseDto>>> GetTaskLists()
    {
        Log.Start();

        var result = await Mediator.Send(new TaskListQuery()).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<TaskListResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}

