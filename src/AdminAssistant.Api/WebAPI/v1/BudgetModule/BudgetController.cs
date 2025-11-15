using AdminAssistant.Modules.BudgetModule.Queries;

namespace AdminAssistant.WebAPI.v1.BudgetModule;

[ApiController]
[Route("api/v1/budget-module/[controller]")]
[ApiExplorerSettings(GroupName = "Budget Module")]
public sealed class BudgetController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all budgets.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="BudgetResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BudgetResponseDto>>> GetBudgets(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BudgetQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBudgetResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
