using AdminAssistant.Modules.DocumentsModule.Queries;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

[ApiController]
[Route("api/v1/document-module/[controller]")]
[ApiExplorerSettings(GroupName = "Documents Module")]
public sealed class DocumentController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all documents.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="DocumentResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DocumentResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DocumentResponseDto>>> GetDocuments(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new DocumentQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToDocumentResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
