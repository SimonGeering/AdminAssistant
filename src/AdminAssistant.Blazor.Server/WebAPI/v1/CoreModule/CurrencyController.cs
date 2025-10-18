using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Queries;

namespace AdminAssistant.WebAPI.v1.CoreModule;

[ApiController]
[Route("api/v1/core-module/[controller]")]
[ApiExplorerSettings(GroupName = "Core Module")]
public sealed class CurrencyController(IMapper mapper, IMediator mediator, ILoggingProvider loggingProvider)
    : WebApiControllerBase(mapper, mediator, loggingProvider)
{
    [HttpPut]
    [SwaggerOperation("Update an existing Currency.", OperationId = "PutCurrency")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyUpdateRequest is invalid.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut([FromBody, SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)] CurrencyUpdateRequestDto currencyUpdateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var currency = currencyUpdateRequest.ToCurrency();
        var result = await Mediator.Send(new CurrencyUpdateCommand(currency), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(NotFound(ModelState));
        }

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState));
        }

        var response = result.Value.ToCurrencyResponseDto();
        return Log.Finish(Ok(response));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new Currency.", OperationId = "PostCurrency")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyCreateRequest is invalid.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost([FromBody, SwaggerParameter("The details of the Currency to be created.", Required = true)] CurrencyCreateRequestDto currencyCreateRequest, CancellationToken cancellationToken)
    {
        Log.Start();

        var currency = Mapper.Map<Currency>(currencyCreateRequest);
        var result = await Mediator.Send(new CurrencyCreateCommand(currency), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
            return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
        }

        var response = Mapper.Map<CurrencyResponseDto>(result.Value);
        return Log.Finish(CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
    }

    [HttpGet("{currencyID}", Name = nameof(CurrencyGetById))]
    [SwaggerOperation("Gets the Currency with the given ID.", OperationId = "GetCurrencyById")]
    [SwaggerResponse(StatusCodes.Status200OK, "OK - returns the Currency requested.", type: typeof(CurrencyResponseDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the given CurrencyID does not exist.")]
    public async Task<ActionResult<CurrencyResponseDto>> CurrencyGetById([SwaggerParameter("The ID of the Currency to be returned.", Required = true)] int currencyID, CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new CurrencyByIDQuery(currencyID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return Log.Finish(NotFound());

        var response = Mapper.Map<CurrencyResponseDto>(result.Value);
        return Log.Finish(Ok(response));
    }

    [HttpGet]
    [SwaggerOperation("Lists all currencies supported by the API wherever a CurrencyID can be provided.", OperationId = "GetCurrency")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns a list of CurrencyResponseDto", type: typeof(IEnumerable<CurrencyResponseDto>))]
    public async Task<ActionResult<IEnumerable<CurrencyResponseDto>>> GetCurrency(CancellationToken cancellationToken)
    {
        Log.Start();

        var result = await Mediator.Send(new CurrenciesQuery(), cancellationToken).ConfigureAwait(false);
        var response = Mapper.Map<IEnumerable<CurrencyResponseDto>>(result.Value);

        return Log.Finish(Ok(response));
    }
}
