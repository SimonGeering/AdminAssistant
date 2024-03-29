using AdminAssistant.Modules.MailModule.Queries;

namespace AdminAssistant.WebAPI.v1.MailModule;

[ApiController]
[Route("api/v1/mail-module/[controller]")]
[ApiExplorerSettings(GroupName = "Mail Module")]
public sealed class MailMessageController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpGet]
    [SwaggerOperation("Lists all mail messages", OperationId = "GetMailMessage")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of MailMessageResponseDto", type: typeof(IEnumerable<MailMessageResponseDto>))]
    public async Task<ActionResult<IEnumerable<MailMessageResponseDto>>> GetMailMessages(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new MailMessageQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<MailMessageResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
