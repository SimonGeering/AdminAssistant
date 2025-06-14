using AdminAssistant;
using AdminAssistant.Services.Core;
using AdminAssistant.Services.Core.CurrencyEndpoints;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

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

app.MapGroup("api/v1/core-module/currency")
    .WithTags("Core Module")
    .MapCurrencyPost()
    .MapCurrencyPut()
    .MapCurrencyGet()
    .MapCurrencyGetById();

await app.RunAsync();
