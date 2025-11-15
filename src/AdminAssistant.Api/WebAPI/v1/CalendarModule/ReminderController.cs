using AdminAssistant.Modules.CalendarModule.Queries;

namespace AdminAssistant.WebAPI.v1.CalendarModule;

[ApiController]
[Route("api/v1/calendar-module/[controller]")]
[ApiExplorerSettings(GroupName = "Calendar Module")]
public sealed class ReminderController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all reminders.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="ReminderResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReminderResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReminderResponseDto>>> GetReminders(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new ReminderQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToReminderResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
