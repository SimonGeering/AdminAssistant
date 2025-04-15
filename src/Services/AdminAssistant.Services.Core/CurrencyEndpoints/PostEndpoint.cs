using AdminAssistant.Modules.CoreModule;
using SimonGeering.Framework.TypeMapping;

namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public static class PostEndpoint
{
    public static IEndpointRouteBuilder MapCurrencyPost(this IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
//     [HttpPost]
//     [SwaggerOperation("Creates a new Currency.", OperationId = "PostCurrency")]
//     [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto))]
//     [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyCreateRequest is invalid.")]
//     public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost([FromBody, SwaggerParameter("The details of the Currency to be created.", Required = true)] CurrencyCreateRequestDto currencyCreateRequest, CancellationToken cancellationToken)
//     {
//         Log.Start();
//
//         var currency = Mapper.Map<Currency>(currencyCreateRequest);
//         var result = await Mediator.Send(new CurrencyCreateCommand(currency), cancellationToken).ConfigureAwait(false);
//
//         if (result.Status == ResultStatus.Invalid)
//         {
//             result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//             return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
//         }
//
//         var response = Mapper.Map<CurrencyResponseDto>(result.Value);
//         return Log.Finish(CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
//     }
//
}

// [SwaggerSchema(Required = new[] { "Symbol", "DecimalFormat" })]
public sealed record CurrencyCreateRequestDto : IMapTo<Currency>
{
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<CurrencyCreateRequestDto, Modules.CoreModule.Currency>()
            .ForMember(x => x.CurrencyID, opt => opt.Ignore());
}
