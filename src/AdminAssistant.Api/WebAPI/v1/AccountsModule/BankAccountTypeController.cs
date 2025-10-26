using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountTypeController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Lists all bank account types supported by the API wherever a BankAccountTypeID can be provided.", OperationId = "GetBankAccountType")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of BankAccountTypeResponseDto", type: typeof(IEnumerable<BankAccountTypeResponseDto>))]
    public async Task<ActionResult<IEnumerable<BankAccountTypeResponseDto>>> BankAccountTypeGet(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankAccountTypesQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBankAccountTypeResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
