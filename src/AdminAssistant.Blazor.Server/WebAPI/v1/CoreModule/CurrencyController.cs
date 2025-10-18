using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Queries;

namespace AdminAssistant.WebAPI.v1.CoreModule;

[ApiController]
[Route("api/v1/core-module/[controller]")]
[ApiExplorerSettings(GroupName = "Core Module")]
public sealed class CurrencyController(IMediator mediator, ILoggingProvider log) : ControllerBase
{
    [HttpPut]
    [SwaggerOperation("Update an existing Currency.", OperationId = "PutCurrency")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyUpdateRequest is invalid.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut([FromBody, SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)] CurrencyUpdateRequestDto currencyUpdateRequest, CancellationToken cancellationToken)
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

    [HttpPost]
    [SwaggerOperation("Creates a new Currency.", OperationId = "PostCurrency")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyCreateRequest is invalid.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost([FromBody, SwaggerParameter("The details of the Currency to be created.", Required = true)] CurrencyCreateRequestDto currencyCreateRequest, CancellationToken cancellationToken)
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

    [HttpGet("{currencyID}", Name = nameof(CurrencyGetById))]
    [SwaggerOperation("Gets the Currency with the given ID.", OperationId = "GetCurrencyById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Currency requested.", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given CurrencyID does not exist.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyGetById([SwaggerParameter("The ID of the Currency to be returned.", Required = true)] int currencyID, CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new CurrencyByIDQuery(currencyID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(NotFound());

        var response = result.Value.ToCurrencyResponseDto();
        return log.Finish(Ok(response));
    }

    [HttpGet]
    [SwaggerOperation("Lists all currencies supported by the API wherever a CurrencyID can be provided.", OperationId = "GetCurrency")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of CurrencyResponseDto", type: typeof(IEnumerable<CurrencyResponseDto>))]
    public async Task<ActionResult<IEnumerable<CurrencyResponseDto>>> GetCurrency(CancellationToken cancellationToken)
    {
        log.Start();

        var result = await mediator.Send(new CurrenciesQuery(), cancellationToken).ConfigureAwait(false);
        var response = result.Value.ToCurrencyList();

        return log.Finish(Ok(response));
    }
}
