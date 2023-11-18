using AdminAssistant.DomainModel.Modules.MailModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    public async Task<ActionResult<IEnumerable<MailMessageResponseDto>>> GetMailMessages()
    {
        Log.Start();

        var result = await Mediator.Send(new MailMessageQuery()).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<MailMessageResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
