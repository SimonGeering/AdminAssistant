using AdminAssistant;
using AdminAssistant.Core.API;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Framework.TypeMapping;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using AutoMapper;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
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


//    [HttpPut]
//    [SwaggerOperation("Update an existing Currency.", OperationId = "PutCurrency")]
//    [SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto))]
//    [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist.")]
//    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyUpdateRequest is invalid.")]
//    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut([FromBody, SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)] CurrencyUpdateRequestDto currencyUpdateRequest)
//    {
//        Log.Start();

//        var currency = Mapper.Map<Currency>(currencyUpdateRequest);
//        var result = await Mediator.Send(new CurrencyUpdateCommand(currency)).ConfigureAwait(false);

//        if (result.Status == ResultStatus.NotFound)
//        {
//            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//            return Log.Finish(NotFound(ModelState));
//        }

//        if (result.Status == ResultStatus.Invalid)
//        {
//            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//            return Log.Finish(UnprocessableEntity(ModelState));
//        }

//        var response = Mapper.Map<CurrencyResponseDto>(result.Value);
//        return Log.Finish(Ok(response));
//    }

//    [HttpPost]
//    [SwaggerOperation("Creates a new Currency.", OperationId = "PostCurrency")]
//    [SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto))]
//    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyCreateRequest is invalid.")]
//    public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost([FromBody, SwaggerParameter("The details of the Currency to be created.", Required = true)] CurrencyCreateRequestDto currencyCreateRequest)
//    {
//        Log.Start();

//        var currency = Mapper.Map<Currency>(currencyCreateRequest);
//        var result = await Mediator.Send(new CurrencyCreateCommand(currency)).ConfigureAwait(false);

//        if (result.Status == ResultStatus.Invalid)
//        {
//            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//            return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
//        }

//        var response = Mapper.Map<CurrencyResponseDto>(result.Value);
//        return Log.Finish(CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
//    }

app.MapGet("/v1/currency/{currencyID}",
    async ([SwaggerParameter("The ID of the Currency to be returned.", Required = true)] int currencyID, IMapper mapper, IMediator mediator, ILoggingProvider log) =>
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

namespace AdminAssistant.Core.API
{

    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
        }
    }

    [SwaggerSchema(Required = new[] { "Symbol", "DecimalFormat" })]
    public record CurrencyCreateRequestDto : IMapTo<Currency>
    {
        public string Symbol { get; init; } = string.Empty;
        public string DecimalFormat { get; init; } = string.Empty;

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<CurrencyCreateRequestDto, Currency>()
                      .ForMember(x => x.CurrencyID, opt => opt.Ignore());
    }

    public record CurrencyResponseDto : IMapFrom<Currency>
    {
        public int CurrencyID { get; init; }
        public string Symbol { get; init; } = string.Empty;
        public string DecimalFormat { get; init; } = string.Empty;
    }

    [SwaggerSchema(Required = new[] { "CurrencyID", "Symbol", "DecimalFormat" })]
    public record CurrencyUpdateRequestDto : IMapTo<Currency>
    {
        [SwaggerSchema("The Currency identifier.", ReadOnly = true)]
        public int CurrencyID { get; init; }
        public string Symbol { get; init; } = string.Empty;
        public string DecimalFormat { get; init; } = string.Empty;
    }
}
