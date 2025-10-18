using AdminAssistant.Modules.AccountsModule.Queries;
using AdminAssistant.Shared;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountInfoController(IMediator mediator, ILoggingProvider log, IUserContextProvider userContextProvider) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BankAccountInfoResponseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation("Lists the summary info for all the available BankAccounts owned by the logged in user.", OperationId = "GetBankAccountInfo")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountInfoResponseDto", type: typeof(IEnumerable<BankAccountInfoResponseDto>))]
    public async Task<ActionResult<IEnumerable<BankAccountInfoResponseDto>>> BankAccountInfoGet(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankAccountInfoQuery(userContextProvider.GetCurrentUser().UserID), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBankAccountInfoResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
