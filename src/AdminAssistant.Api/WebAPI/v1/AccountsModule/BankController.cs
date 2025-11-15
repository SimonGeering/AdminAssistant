using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[ApiController]
[Route("api/v1/accounts-module/[controller]")]
[ApiExplorerSettings(GroupName = "Accounts Module")]
public sealed class BankController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Update an existing Bank.
    /// </summary>
    /// <param name="bankUpdateRequest">The Bank for which updates are to be persisted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated <see cref="BankResponseDto"/>.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(BankResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BankResponseDto>> BankPut(
        [FromBody] BankUpdateRequestDto bankUpdateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var bank = bankUpdateRequest.ToBank();
        var result = await mediator.Send(new BankUpdateCommand(bank), cancellationToken).ConfigureAwait(false);

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

        var response = result.Value.ToBankResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Creates a new Bank.
    /// </summary>
    /// <param name="bankCreateRequest">The details of the Bank to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created <see cref="BankResponseDto"/> with its newly assigned ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BankResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BankResponseDto>> BankPost(
        [FromBody] BankCreateRequestDto bankCreateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var bank = bankCreateRequest.ToBank();
        var result = await mediator.Send(new BankCreateCommand(bank), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = result.Value.ToBankResponseDto();
        return log.Finish(CreatedAtRoute(nameof(BankGetById), new { bankID = response.BankID }, response));
    }

    /// <summary>
    /// Gets the Bank with the given ID.
    /// </summary>
    /// <param name="bankID">The ID of the Bank to be returned.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested <see cref="BankResponseDto"/>.</returns>
    [HttpGet("{bankID}", Name = nameof(BankGetById))]
    [ProducesResponseType(typeof(BankResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BankResponseDto>> BankGetById(
        int bankID,
        CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankByIDQuery(bankID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToBankResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Lists all banks.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="BankResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BankResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BankResponseDto>>> BankGet(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new BankQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToBankResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
