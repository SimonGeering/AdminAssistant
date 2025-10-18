using AdminAssistant.Modules.DocumentsModule.Queries;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

[ApiController]
[Route("api/v1/document-module/[controller]")]
[ApiExplorerSettings(GroupName = "Documents Module")]
public sealed class DocumentController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all documents.", OperationId = "GetDocument")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of DocumentResponseDto", type: typeof(IEnumerable<DocumentResponseDto>))]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetDocuments(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new DocumentQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToDocumentResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
