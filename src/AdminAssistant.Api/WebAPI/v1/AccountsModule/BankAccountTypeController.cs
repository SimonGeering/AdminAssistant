using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountTypeController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Lists all bank account types supported by the API wherever a BankAccountTypeID can be provided.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="BankAccountTypeResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BankAccountTypeResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BankAccountTypeResponseDto>>> BankAccountTypeGet(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankAccountTypesQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBankAccountTypeResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
