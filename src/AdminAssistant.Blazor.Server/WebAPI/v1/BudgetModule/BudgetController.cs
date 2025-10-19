using AdminAssistant.Modules.BudgetModule.Queries;

namespace AdminAssistant.WebAPI.v1.BudgetModule;

[ApiController]
[Route("api/v1/budget-module/[controller]")]
[ApiExplorerSettings(GroupName = "Budget Module")]
public sealed class BudgetController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all budgets.", OperationId = "GetBudget")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BudgetResponseDto", type: typeof(IEnumerable<BudgetResponseDto>))]
    public async Task<ActionResult<IEnumerable<BudgetResponseDto>>> GetBudgets(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BudgetQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBudgetResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
