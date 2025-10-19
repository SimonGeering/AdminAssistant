using AdminAssistant.Modules.MailModule.Queries;

namespace AdminAssistant.WebAPI.v1.MailModule;

[ApiController]
[Route("api/v1/mail-module/[controller]")]
[ApiExplorerSettings(GroupName = "Mail Module")]
public sealed class MailMessageController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all mail messages", OperationId = "GetMailMessage")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of MailMessageResponseDto", type: typeof(IEnumerable<MailMessageResponseDto>))]
    public async Task<ActionResult<IEnumerable<MailMessageResponseDto>>> GetMailMessages(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new MailMessageQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToMailMessageResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
