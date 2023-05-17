using AdminAssistant.DomainModel.Modules.BudgetModule.CQRS;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.BudgetModule;

[ApiController]
[Route("api/v1/budget-module/[controller]")]
[ApiExplorerSettings(GroupName = "Budget Module")]
public sealed class BudgetController : WebAPIControllerBase
{
    public BudgetController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
        : base(mapper, mediator, loggingProvider)
    {
    }

    [HttpGet]
    [SwaggerOperation("Lists all budgets.", OperationId = "GetBudget")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BudgetResponseDto", type: typeof(IEnumerable<BudgetResponseDto>))]
    public async Task<ActionResult<IEnumerable<BudgetResponseDto>>> GetBudgets()
    {
        Log.Start();

        var result = await Mediator.Send(new BudgetQuery()).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<BudgetResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
