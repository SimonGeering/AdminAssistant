using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.Shared;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountInfoController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider, IUserContextProvider userContextProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BankAccountInfoResponseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation("Lists the summary info for all the available BankAccounts owned by the logged in user.", OperationId = "GetBankAccountInfo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountInfoResponseDto", type: typeof(IEnumerable<BankAccountInfoResponseDto>))]
    public async Task<ActionResult<IEnumerable<BankAccountInfoResponseDto>>> BankAccountInfoGet(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new BankAccountInfoQuery(userContextProvider.GetCurrentUser().UserID), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<BankAccountInfoResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
