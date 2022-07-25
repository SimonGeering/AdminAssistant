using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.Core.API;

public static class CurrencyEndpoint
{
    public static void MapCurrencyEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/v1/currency",
            async ([SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)]
                   CurrencyUpdateRequestDto currencyUpdateRequest, IMapper mapper, IMediator mediator, ILoggingProvider log) =>
            {
                log.Start();

                var currency = mapper.Map<Currency>(currencyUpdateRequest);
                var result = await mediator.Send(new CurrencyUpdateCommand(currency)).ConfigureAwait(false);

                if (result.Status == ResultStatus.NotFound)
                    return log.Finish(Results.NotFound());

                if (result.Status == ResultStatus.Invalid)
                    return log.Finish(Results.ValidationProblem(result.ValidationErrors.ToErrorDetails())); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request

                var response = mapper.Map<CurrencyResponseDto>(result.Value);
                return log.Finish(Results.Ok(response));
            })
            .WithName("PutCurrency")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Update an existing Currency."))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto)))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist."))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status400BadRequest, "BadRequest - When the given currencyUpdateRequest is invalid."));

        app.MapPost("/v1/currency",
            async ([SwaggerParameter("The details of the Currency to be created.", Required = true)]
                   CurrencyCreateRequestDto currencyCreateRequest, IMapper mapper, IMediator mediator, ILoggingProvider log) =>
            {
                log.Start();

                var currency = mapper.Map<Currency>(currencyCreateRequest);
                var result = await mediator.Send(new CurrencyCreateCommand(currency)).ConfigureAwait(false);

                if (result.Status == ResultStatus.Invalid)
                    return log.Finish(Results.ValidationProblem(result.ValidationErrors.ToErrorDetails())); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request

                var response = mapper.Map<CurrencyResponseDto>(result.Value);
                return log.Finish(Results.CreatedAtRoute("CurrencyGetById", new { currencyID = response.CurrencyID }, response));
            })
            .WithName("PostCurrency")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Creates a new Currency."))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto)))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status400BadRequest, "BadRequest - When the given currencyCreateRequest is invalid.", type: typeof(CurrencyResponseDto)));

        app.MapGet("/v1/currency/{currencyID}",
            async ([SwaggerParameter("The ID of the Currency to be returned.", Required = true)]
                   int currencyID, IMapper mapper, IMediator mediator, ILoggingProvider log) =>
            {
                log.Start();

                var result = await mediator.Send(new CurrencyByIDQuery(currencyID)).ConfigureAwait(false);

                if (result.Status == ResultStatus.NotFound)
                    return log.Finish(Results.NotFound());

                var response = mapper.Map<CurrencyResponseDto>(result.Value);
                return log.Finish(Results.Ok(response));
            })
            .WithName("CurrencyGetById")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Gets the Currency with the given ID."))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status200OK, "OK - returns the Currency requested.", type: typeof(CurrencyResponseDto)))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status404NotFound, "NotFound - When the given CurrencyID does not exist.", type: typeof(CurrencyResponseDto)));

        app.MapGet("/v1/currency",
            async (IMapper mapper, IMediator mediator, ILoggingProvider log) =>
            {
                log.Start();

                var result = await mediator.Send(new CurrenciesQuery()).ConfigureAwait(false);
                var response = mapper.Map<IEnumerable<CurrencyResponseDto>>(result.Value);

                return log.Finish(Results.Ok(response));
            })
            .WithName("GetCurrency")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Lists all currencies supported by the API wherever a CurrencyID can be provided."))
            .WithMetadata(new SwaggerResponseAttribute(StatusCodes.Status200OK, "Ok - returns a list of CurrencyResponseDto", type: typeof(IEnumerable<CurrencyResponseDto>)));
    }
}
