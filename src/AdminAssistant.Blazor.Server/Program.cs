using AdminAssistant.Shared;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// TODO: Make version a constant shared with WebAPI Assemblies
const string WebAPIVersion = "v1";
const string WebAPITitle = $"Admin Assistant WebAPI {WebAPIVersion}.";

var configSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
Guard.Against.Null(configSettings, nameof(configSettings), "Failed to load configuration settings");

builder.Services.AddMvc(opts =>
{
    // Define MediaType limits ...
    opts.Filters.Add(new ProducesAttribute("application/json")); // Response limit
    opts.Filters.Add(new ConsumesAttribute("application/json")); // Request limit
    opts.ReturnHttpNotAcceptable = true; // Force client to only request media types based on the above limits.
});
builder.Services.AddResponseCompression(opts
    => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));

//// TODO: OIDC https://damienbod.com/2021/04/12/securing-blazor-web-assembly-using-cookies-and-auth0/
//// TODO: investigate https://damienbod.com/2021/03/08/securing-blazor-web-assembly-using-cookies/
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<SimonGeering.Framework.Primitives.IPersistable>();

builder.Services.AddSwaggerGen(c =>
{
    // See https://github.com/domaindrivendev/Swashbuckle.AspNetCore for an overview of options available here.
    // https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters - examples for getting swagger to do what you want

    // Skip documenting any controller without a Group name from the ApiExplorerSettings attribute ...
    c.DocInclusionPredicate((_, api) => string.IsNullOrWhiteSpace(api.GroupName) == false);
    c.TagActionsBy(api =>
    {
        // Group by Group name from the ApiExplorerSettings attribute ...
        Guard.Against.Null(api.GroupName, nameof(api.GroupName)); // Should be covered by DocInclusionPredicate.
        return new string[] { api.GroupName };
    });
    c.SwaggerDoc(WebAPIVersion, new OpenApiInfo { Title = WebAPITitle, Version = WebAPIVersion }); // Add OpenAPI/Swagger middleware

    // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
    c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
});
builder.Services.AddFluentValidationRulesToSwagger(); // Adds fluent validation rules to swagger schema See: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
builder.Services.AddAutoMapper(typeof(AdminAssistant.Domain.MappingProfile), typeof(AdminAssistant.Infrastructure.MappingProfile), typeof(AdminAssistant.WebAPI.v1.MappingProfile));
builder.Services.AddAdminAssistantServerSideProviders();
builder.Services.AddAdminAssistantServerSideDomainModel();
builder.Services.AddAdminAssistantApplication();
builder.Services.AddAdminAssistantServerSideInfra(configSettings);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseResponseCompression();
    //    // TODO: put the error page back but without bootstrap.
    //    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Add OpenAPI/Swagger middleware ...

// Serves the registered OpenAPI/Swagger documents on `/swagger/v1/swagger.json`
app.UseSwagger();
//app.UseSwagger(c => c.SerializeAsV2 = true); // Needed for the VS2019 to be able to generate a REST Client.

// Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", WebAPITitle);
    c.RoutePrefix = "api/docs";
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api") == false, app =>
{
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseEndpoints(config => config.MapFallbackToFile("index.html"));
});
app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api"), api =>
{
    api.UseStaticFiles();
    api.UseRouting();
    api.UseEndpoints(config =>
    {
        config.MapRazorPages();
        config.MapControllers();
    });
});

app.Run();

public partial class Program
{
    protected Program() { }
}
