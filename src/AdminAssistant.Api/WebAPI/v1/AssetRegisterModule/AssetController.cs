using AdminAssistant.Modules.AssetRegisterModule.Queries;

namespace AdminAssistant.WebAPI.v1.AssetRegisterModule;

[ApiController]
[Route("api/v1/assetregister-module/[controller]")]
[ApiExplorerSettings(GroupName = "Asset Register Module")]
public sealed class AssetController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all assets.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="AssetResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AssetResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AssetResponseDto>>> GetAssets(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new AssetQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBudgetResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
