using AdminAssistant.Modules.CalendarModule.Queries;

namespace AdminAssistant.WebAPI.v1.CalendarModule;

[ApiController]
[Route("api/v1/calendar-module/[controller]")]
[ApiExplorerSettings(GroupName = "Calendar Module")]
public sealed class ReminderController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpGet]
    [SwaggerOperation("Lists all reminders.", OperationId = "GetReminder")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of ReminderResponseDto", type: typeof(IEnumerable<ReminderResponseDto>))]
    public async Task<ActionResult<IEnumerable<ReminderResponseDto>>> GetReminders(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new ReminderQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<ReminderResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
