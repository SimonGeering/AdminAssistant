#pragma warning disable IDE0053 // Use expression body for lambda expressions
using AdminAssistant.Core.API;
using AdminAssistant.DomainModel.Shared;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using OpenTelemetry.Metrics;
//using OpenTelemetry.Resources;
//using OpenTelemetry.Trace;

//const string SourceName = "AdminAssistant.Core.API";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    // See https://github.com/domaindrivendev/Swashbuckle.AspNetCore for an overview of options available here.

    // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
    options.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
});
//builder.Services.AddOpenTelemetryTracing(config =>
//{
//    config.AddSource(SourceName);
//    config.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(SourceName).AddTelemetrySdk());
//    config.AddSqlClientInstrumentation(options =>
//    {
//        options.SetDbStatementForText = true;
//        options.RecordException = true;
//    });
//    config.AddAspNetCoreInstrumentation(options =>
//    {
//        options.Filter = (req) => !req.Request.Path.ToUriComponent().Contains("index.html", StringComparison.OrdinalIgnoreCase)
//                                          && !req.Request.Path.ToUriComponent().Contains("swagger", StringComparison.OrdinalIgnoreCase);
//    });
//    config.AddHttpClientInstrumentation();
//    config.AddOtlpExporter(otlpOptions =>
//    {
//        otlpOptions.Endpoint = new Uri("http://adminassistant.opentelemetry.collector");
//    });
//});
//builder.Services.AddOpenTelemetryMetrics(builder =>
//{
//    builder.AddHttpClientInstrumentation();
//    builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(SourceName).AddTelemetrySdk());

//});

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

app.MapCurrencyEndpoint();

app.Run();

#pragma warning restore IDE0053 // Use expression body for lambda expressions
