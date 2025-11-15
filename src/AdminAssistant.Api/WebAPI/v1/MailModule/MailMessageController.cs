using AdminAssistant.Modules.MailModule.Queries;

namespace AdminAssistant.WebAPI.v1.MailModule;

[ApiController]
[Route("api/v1/mail-module/[controller]")]
[ApiExplorerSettings(GroupName = "Mail Module")]
public sealed class MailMessageController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all mail messages.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="MailMessageResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MailMessageResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MailMessageResponseDto>>> GetMailMessages(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new MailMessageQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToMailMessageResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
