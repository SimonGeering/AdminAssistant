namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public static class PutEndpoint
{
    public static IEndpointRouteBuilder MapCurrencyPut(this IEndpointRouteBuilder endpoints)
    {
        // Your implementation logic here
        return endpoints;
    }

//     [HttpPut]
//     [SwaggerOperation("Update an existing Currency.", OperationId = "PutCurrency")]
//     [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto))]
//     [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist.")]
//     [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyUpdateRequest is invalid.")]
//     public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut([FromBody, SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)] CurrencyUpdateRequestDto currencyUpdateRequest, CancellationToken cancellationToken)
//     {
//         Log.Start();
//
//         var currency = Mapper.Map<Currency>(currencyUpdateRequest);
//         var result = await Mediator.Send(new CurrencyUpdateCommand(currency), cancellationToken).ConfigureAwait(false);
//
//         if (result.Status == ResultStatus.NotFound)
//         {
//             result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//             return Log.Finish(NotFound(ModelState));
//         }
//
//         if (result.Status == ResultStatus.Invalid)
//         {
//             result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//             return Log.Finish(UnprocessableEntity(ModelState));
//         }
//
//         var response = Mapper.Map<CurrencyResponseDto>(result.Value);
//         return Log.Finish(Ok(response));
//     }
}

//[SwaggerSchema(Required = new[] { "CurrencyID", "Symbol", "DecimalFormat" })]
public sealed record CurrencyUpdateRequestDto // : IMapTo<Currency>
{
    //[SwaggerSchema("The Currency identifier.", ReadOnly = true)]
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}
