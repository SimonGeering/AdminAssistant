using AdminAssistant.Modules.CalendarModule.Queries;

namespace AdminAssistant.WebAPI.v1.CalendarModule;

[ApiController]
[Route("api/v1/calendar-module/[controller]")]
[ApiExplorerSettings(GroupName = "Calendar Module")]
public sealed class ReminderController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all reminders.", OperationId = "GetReminder")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of ReminderResponseDto", type: typeof(IEnumerable<ReminderResponseDto>))]
    public async Task<ActionResult<IEnumerable<ReminderResponseDto>>> GetReminders(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ReminderQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToReminderResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
