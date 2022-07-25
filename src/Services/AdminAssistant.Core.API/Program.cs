using AdminAssistant;
using AdminAssistant.Core.API;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using AutoMapper;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    // See https://github.com/domaindrivendev/Swashbuckle.AspNetCore for an overview of options available here.

    // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
    c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
});
builder.Services.AddAutoMapper(typeof(AdminAssistant.Infra.DAL.MappingProfile), typeof(MappingProfile));

builder.Services.AddAdminAssistantServerSideProviders();
builder.Services.AddAdminAssistantServerSideDomainModel();

var config = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
builder.Services.AddAdminAssistantServerSideInfra(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () =>
{
    var Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
     .ToArray();
})
.WithName("GetWeatherForecast");

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

app.Run();
