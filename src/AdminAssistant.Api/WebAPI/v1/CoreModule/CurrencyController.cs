using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Queries;

namespace AdminAssistant.WebAPI.v1.CoreModule;

[ApiController]
[Route("api/v1/core-module/[controller]")]
[ApiExplorerSettings(GroupName = "Core Module")]
public sealed class CurrencyController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    /// <summary>
    /// Update an existing Currency.
    /// </summary>
    /// <param name="currencyUpdateRequest">The Currency for which updates are to be persisted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated <see cref="CurrencyResponseDto"/>.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(CurrencyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut(
        [FromBody] CurrencyUpdateRequestDto currencyUpdateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var currency = currencyUpdateRequest.ToCurrency();
        var result = await mediator.Send(new CurrencyUpdateCommand(currency), cancellationToken).ConfigureAwait(false);

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

        var response = result.Value.ToCurrencyResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Creates a new Currency.
    /// </summary>
    /// <param name="currencyCreateRequest">The details of the Currency to be created.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created <see cref="CurrencyResponseDto"/> with its newly assigned ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CurrencyResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost(
        [FromBody] CurrencyCreateRequestDto currencyCreateRequest,
        CancellationToken cancellationToken)
    {
        log.Start();

        var currency = currencyCreateRequest.ToCurrency();
        var result = await mediator.Send(new CurrencyCreateCommand(currency), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = result.Value.ToCurrencyResponseDto();
        return log.Finish(CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
    }

    /// <summary>
    /// Gets the Currency with the given ID.
    /// </summary>
    /// <param name="currencyID">The ID of the Currency to be returned.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested <see cref="CurrencyResponseDto"/>.</returns>
    [HttpGet("{currencyID}", Name = nameof(CurrencyGetById))]
    [ProducesResponseType(typeof(CurrencyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyGetById(
        int currencyID,
        CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new CurrencyByIDQuery(currencyID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToCurrencyResponseDto();
        return log.Finish(Ok(response));
    }

    /// <summary>
    /// Lists all currencies supported by the API wherever a CurrencyID can be provided.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of <see cref="CurrencyResponseDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CurrencyResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CurrencyResponseDto>>> GetCurrency(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new CurrenciesQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToCurrencyResponseDtoEnumeration();

        return log.Finish(Ok(response));
    }
}
