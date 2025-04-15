using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public static class GetEndpoint
{
    public static IEndpointRouteBuilder MapCurrencyGet(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(string.Empty, async Task<Ok<IEnumerable<CurrencyResponseDto>>> (CancellationToken cancellationToken,
                [FromServices] IMapper mapper, [FromServices] IMediator mediator, [FromServices] ILoggingProvider log) =>
            {
                log.Start();

                var result = await mediator.Send(new CurrenciesQuery(), cancellationToken).ConfigureAwait(false);
                var response = mapper.Map<IEnumerable<CurrencyResponseDto>>(result.Value);

                return log.Finish(TypedResults.Ok(response));
            })
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(cfg =>
            {
                cfg.OperationId = "GetCurrency";
                cfg.Summary = "Get Currencies";
                cfg.Description = "Lists all currencies supported by the API wherever a CurrencyID can be provided.";

                cfg.Responses[StatusCodes.Status200OK.ToString()].Description = "OK - Returns a list of CurrencyResponseDto.";
                return cfg;
            });

        return endpoints;
    }
}
