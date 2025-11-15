using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankAccountController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Update an existing BankAccount.
    /// </summary>
    /// <param name="bankAccountUpdateRequest">The BankAccount for which updates are to be persisted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated <see cref="BankAccountResponseDto"/>.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountPut(
        [FromBody] BankAccountUpdateRequestDto bankAccountUpdateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var bankAccount = bankAccountUpdateRequest.ToBankAccount();
        var result = await mediator.Send(new BankAccountUpdateCommand(bankAccount), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(NotFound(ModelState));
        }

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState));
        }

        var response = result.Value.ToBankAccountResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Creates a new BankAccount.
    /// </summary>
    /// <param name="bankAccountCreateRequest">The details of the BankAccount to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created <see cref="BankAccountResponseDto"/> with its newly assigned ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountPost(
        [FromBody] BankAccountCreateRequestDto bankAccountCreateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var bankAccount = bankAccountCreateRequest.ToBankAccount();
        var result = await mediator.Send(new BankAccountCreateCommand(bankAccount), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = result.Value.ToBankAccountResponseDto();
        return log.Finish(CreatedAtRoute(nameof(BankAccountGetById), new { bankAccountID = response.BankAccountID }, response));
    }

    /// <summary>
    /// Gets the <see cref="BankAccountResponseDto"/> with the given ID.
    /// </summary>
    /// <param name="bankAccountID">The ID of the BankAccount to be returned.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested <see cref="BankAccountResponseDto"/>.</returns>
    [HttpGet("{bankAccountID}")]
    [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BankAccountResponseDto>> BankAccountGetById(
        int bankAccountID,
        CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankAccountByIDQuery(bankAccountID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToBankAccountResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Get the transactions since the last bank account statement for the BankAccount with the given ID.
    /// </summary>
    /// <param name="bankAccountID">The ID of the BankAccount.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="BankAccountTransactionResponseDto"/>.</returns>
    [HttpGet("{bankAccountID}/transactions")]
    [ProducesResponseType(typeof(IEnumerable<BankAccountTransactionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BankAccountTransactionResponseDto>>> BankAccountTransactionsGetByBankAccountID(
        int bankAccountID,
        CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankAccountTransactionsByBankAccountIDQuery(bankAccountID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToBankAccountTransactionResponseDtoEnumeration();
        return log.Finish(Ok(response));
    }
}
