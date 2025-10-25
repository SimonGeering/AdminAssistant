using AdminAssistant.Modules.AssetRegisterModule.Queries;

namespace AdminAssistant.WebAPI.v1.AssetRegisterModule;

[ApiController]
[Route("api/v1/assetregister-module/[controller]")]
[ApiExplorerSettings(GroupName = "Asset Register Module")]
public sealed class AssetController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all assets", OperationId = "GetAsset")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of AssetResponseDto", type: typeof(IEnumerable<AssetResponseDto>))]
    public async Task<ActionResult<IEnumerable<AssetResponseDto>>> GetAssets(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new AssetQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBudgetResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
