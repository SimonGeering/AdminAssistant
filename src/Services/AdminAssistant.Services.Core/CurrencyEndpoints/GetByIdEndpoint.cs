using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule.Queries;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Services.Core.CurrencyEndpoints;

public static class GetByIdEndpoint
{
    public static IEndpointRouteBuilder MapCurrencyGetById(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{currencyID}", async Task<Results<Ok<CurrencyResponseDto>, NotFound>> (
                [FromQuery] int currencyID, CancellationToken cancellationToken,
                [FromServices] IMapper mapper,
                [FromServices] IMediator mediator,
                [FromServices] ILoggingProvider log) =>
            {
                log.Start();

                var result = await mediator.Send(new CurrencyByIDQuery(currencyID), cancellationToken)
                    .ConfigureAwait(false);

                if (result.Status == ResultStatus.NotFound)
                    return log.Finish(TypedResults.NotFound());

                var response = mapper.Map<CurrencyResponseDto>(result.Value);
                return log.Finish(TypedResults.Ok(response));
            })
            .WithOpenApi(cfg =>
            {
                var currencyId = cfg.Parameters[0];
                currencyId.Description = "The ID of the Currency to be returned.";
                currencyId.Required = true;

                cfg.OperationId = "GetCurrencyById";
                cfg.Summary = "Get Currency By Id";
                cfg.Description = "Gets the Currency with the given ID.";

                cfg.Responses[StatusCodes.Status200OK.ToString()].Description = "OK - Returns a CurrencyResponseDto for the Currency requested.";
                cfg.Responses[StatusCodes.Status404NotFound.ToString()].Description = "Not Found - When the given CurrencyID does not exist.";
                return cfg;
            })
            .Produces(StatusCodes.Status500InternalServerError);

        return endpoints;
    }
}
