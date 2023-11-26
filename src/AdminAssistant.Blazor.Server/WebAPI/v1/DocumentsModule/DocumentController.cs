using AdminAssistant.DomainModel.Modules.DocumentsModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

[ApiController]
[Route("api/v1/document-module/[controller]")]
[ApiExplorerSettings(GroupName = "Documents Module")]
public sealed class DocumentController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpGet]
    [SwaggerOperation("Lists all documents.", OperationId = "GetDocument")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of DocumentResponseDto", type: typeof(IEnumerable<DocumentResponseDto>))]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetDocuments(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new DocumentQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<DocumentResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
