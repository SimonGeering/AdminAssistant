using AdminAssistant;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Queries;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using SimonGeering.Framework.TypeMapping;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails(); // https://www.strathweb.com/2022/08/problem-details-responses-everywhere-with-asp-net-core-and-net-7/

if (builder.Environment.IsDevelopment())
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

builder.Services.AddResponseCompression(opts
    => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]));

builder.Services.AddAutoMapper(typeof(AdminAssistant.Domain.MappingProfile), typeof(AdminAssistant.Infrastructure.MappingProfile), typeof(MappingProfile));
builder.Services.AddAdminAssistantServerSideProviders();
builder.Services.AddAdminAssistantServerSideDomainModel();
builder.Services.AddAdminAssistantApplication();
builder.Services.AddAdminAssistantServerSideInfra();

builder.AddAdminAssistantApplicationDbContext();

var app = builder.Build();

app.MapDefaultEndpoints();

// https://www.strathweb.com/2022/08/problem-details-responses-everywhere-with-asp-net-core-and-net-7/
app.UseExceptionHandler();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(cfg =>
    {
        cfg.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
        {
            if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host")) return;
            var basePath = "proxy";
            var serverUrl = $"{httpRequest.Scheme}://{httpRequest.Headers["X-Forwarded-Host"]}{Constants.ApiGateway.CoreApiPrefix}/";
            swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
        });
    });

    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    });
    app.UseDeveloperExceptionPage();
}

app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHttpsRedirection();

app.UseResponseCompression();

var currencyEndpoints = app.MapGroup("/currency/v1")
    .WithTags("Core Module");

//[HttpPost]
//[SwaggerOperation("Creates a new Currency.", OperationId = "PostCurrency")]
//[SwaggerResponse(StatusCodes.Status201Created, "Created - returns the created currency with its assigned newly ID.", type: typeof(CurrencyResponseDto))]
//[SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyCreateRequest is invalid.")]
//public async Task<ActionResult<CurrencyResponseDto>> CurrencyPost([FromBody, SwaggerParameter("The details of the Currency to be created.", Required = true)] CurrencyCreateRequestDto currencyCreateRequest, CancellationToken cancellationToken)
//{
//    Log.Start();

//    var currency = Mapper.Map<Currency>(currencyCreateRequest);
//    var result = await Mediator.Send(new CurrencyCreateCommand(currency), cancellationToken).ConfigureAwait(false);

//    if (result.Status == ResultStatus.Invalid)
//    {
//        result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//        return Log.Finish(UnprocessableEntity(ModelState)); // https://stackoverflow.com/questions/47269601/what-http-response-code-to-use-for-failed-post-request
//    }

//    var response = Mapper.Map<CurrencyResponseDto>(result.Value);
//    return Log.Finish(CreatedAtRoute(nameof(CurrencyGetById), new { currencyID = response.CurrencyID }, response));
//}

//[SwaggerOperation("Update an existing Currency.", OperationId = "PutCurrency")]
//[SwaggerResponse(StatusCodes.Status200OK, "Ok - returns the updated CurrencyResponseDto", type: typeof(CurrencyResponseDto))]
//[SwaggerResponse(StatusCodes.Status404NotFound, "NotFound - When the CurrencyID of the given currencyUpdateRequest does not exist.")]
//[SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "UnprocessableEntity - When the given currencyUpdateRequest is invalid.")]
//public async Task<ActionResult<CurrencyResponseDto>> CurrencyPut([FromBody, SwaggerParameter("The Currency for which updates are to be persisted.", Required = true)] CurrencyUpdateRequestDto currencyUpdateRequest, CancellationToken cancellationToken)
//currencyEndpoints.MapPut(string.Empty, async Task<Results<Ok<CurrencyResponseDto>>>(
//    CurrencyUpdateRequestDto currencyUpdateRequest, CancellationToken cancellationToken,
//    [FromServices] IMapper mapper, [FromServices] IMediator mediator, [FromServices] ILoggingProvider log) =>
//    {
//        log.Start();

//        var currency = mapper.Map<Currency>(currencyUpdateRequest);
//        var result = await mediator.Send(new CurrencyUpdateCommand(currency), cancellationToken).ConfigureAwait(false);

//        if (result.Status == ResultStatus.NotFound)
//        {
//            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//            return log.Finish(TypedResults.NotFound(ModelState));
//        }

//        if (result.Status == ResultStatus.Invalid)
//        {
//            result.ValidationErrors.ToList().ForEach((err) => ModelState.AddModelError(err.Identifier, err.ErrorMessage));
//            return log.Finish(TypedResults.UnprocessableEntity(ModelState));
//        }

//        var response = mapper.Map<CurrencyResponseDto>(result.Value);
//        return log.Finish(TypedResults.Ok(response));
//    });

currencyEndpoints.MapGet("/{currencyID}", async Task<Results<Ok<CurrencyResponseDto>, NotFound>> (
        [FromQuery] int currencyID, CancellationToken cancellationToken,
        [FromServices] IMapper mapper, [FromServices] IMediator mediator, [FromServices] ILoggingProvider log) =>
    {
        log.Start();

        var result = await mediator.Send(new CurrencyByIDQuery(currencyID), cancellationToken).ConfigureAwait(false);

        if (result.Status == ResultStatus.NotFound)
            return log.Finish(TypedResults.NotFound());

        var response = mapper.Map<CurrencyResponseDto>(result.Value);
        return log.Finish(TypedResults.Ok(response));
    })
    .Produces(StatusCodes.Status500InternalServerError)
    .WithOpenApi(cfg =>
    {
        cfg.OperationId = "GetCurrencyById";
        cfg.Summary = "Get Currency By Id";
        cfg.Description = "Gets the Currency with the given ID.";

        var currencyID = cfg.Parameters[0];
        currencyID.Description = "The ID associated with the created Todo";
        currencyID.Required = true;
        cfg.Responses[StatusCodes.Status200OK.ToString()].Description = "OK - Returns a CurrencyResponseDto for the Currency requested.";
        cfg.Responses[StatusCodes.Status404NotFound.ToString()].Description = "Not Found - When the given CurrencyID does not exist.";
        return cfg;
    });


currencyEndpoints.MapGet(string.Empty, async Task<Ok<IEnumerable<CurrencyResponseDto>>> (CancellationToken cancellationToken,
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

app.Run();

public sealed record CurrencyCreateRequestDto : IMapTo<Currency>
{
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<CurrencyCreateRequestDto, Currency>()
                  .ForMember(x => x.CurrencyID, opt => opt.Ignore());
}

public sealed record CurrencyResponseDto : IMapFrom<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}

public sealed record CurrencyUpdateRequestDto : IMapTo<Currency>
{
    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
}

public sealed class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
