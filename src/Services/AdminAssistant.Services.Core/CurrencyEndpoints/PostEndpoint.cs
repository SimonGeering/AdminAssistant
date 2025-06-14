using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Commands;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimonGeering.Framework.TypeMapping;

namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public static class PostEndpoint
{
    public static IEndpointRouteBuilder MapCurrencyPost(this IEndpointRouteBuilder endpoints)
    {
#pragma warning disable S125
        //    endpoints.MapPost(string.Empty, PostCurrency);
#pragma warning restore S125
        return endpoints;
    }

#pragma warning disable S125
     // internal static async Task<Results<CurrencyResponseDto>> PostCurrency(
     //     CurrencyCreateRequestDto currencyCreateRequest,
     //     CancellationToken cancellationToken,
     //     [FromServices] IMapper mapper,
     //     [FromServices] IMediator mediator,
     //     [FromServices] ILoggingProvider log)
     // {
     //     log.Start();
     //
     //     var currency = mapper.Map<Currency>(currencyCreateRequest);
     //     var result = await mediator.Send(new CurrencyCreateCommand(currency), cancellationToken).ConfigureAwait(false);
     //
     //     if (result.Status == ResultStatus.Invalid)
     //     {
     //         return log.Finish(TypedResults.UnprocessableEntity(result.ValidationErrors));
     //
     //         //result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
     //         //return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
     //     }
     //
     //     var response = mapper.Map<CurrencyResponseDto>(result.Value);
     //     return log.Finish(TypedResults.CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
     // }
     //
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
#pragma warning restore S125
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
