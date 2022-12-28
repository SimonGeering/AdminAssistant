using AdminAssistant.DomainModel.Shared;
using Ardalis.GuardClauses;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace AdminAssistant.Blazor.Server;

public sealed class Startup
{
    // TODO: Make version a constant shared with WebAPI Assemblies
    const string HealthCheckAPI = "/api/health";
    private const string WebAPIVersion = "v1";
    private string WebAPITitle => $"Admin Assistant WebAPI {WebAPIVersion}.";

    private readonly IHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public Startup(IHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        var configSettings = _configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();

        if (configSettings == null)
            throw new NullReferenceException("Failed to load configuration settings");

        services.AddMvc(opts =>
        {
            // Define MediaType limits ...
            opts.Filters.Add(new ProducesAttribute("application/json")); // Response limit
            opts.Filters.Add(new ConsumesAttribute("application/json")); // Request limit
            opts.ReturnHttpNotAcceptable = true; // Force client to only request media types based on the above limits.
        });
        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
        });

        // TODO: OIDC https://damienbod.com/2021/04/12/securing-blazor-web-assembly-using-cookies-and-auth0/
        // TODO: investigate https://damienbod.com/2021/03/08/securing-blazor-web-assembly-using-cookies/
        services.AddHttpContextAccessor();

        services.AddControllers().AddNewtonsoftJson()
          .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Infra.DAL.IDatabasePersistable>());

        services.AddHealthChecks();

        // Remove until .net 7 EF support added see https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/issues/1555
        //services.AddHealthChecksUI(setupSettings: setup =>
        //{
        //    // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
        //    setup.AddHealthCheckEndpoint("Blazor BackEnd Web API", HealthCheckAPI);

        //}).AddInMemoryStorage();

        services.AddSwaggerGen(c =>
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
            //c.AddFluentValidationRules();

            // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
            c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
        });
        services.AddFluentValidationRulesToSwagger(); // Adds fluent validation rules to swagger schema See: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
        services.AddSwaggerGenNewtonsoftSupport();

        services.AddAutoMapper(typeof(Infra.DAL.MappingProfile), typeof(WebAPI.v1.MappingProfile));

        services.AddAdminAssistantServerSideProviders();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantServerSideInfra(configSettings);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseResponseCompression();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }

        // Add OpenAPI/Swagger middleware ...

        // Serves the registered OpenAPI/Swagger documents on `/swagger/v1/swagger.json`
        app.UseSwagger(c => c.SerializeAsV2 = true); // Needed for the VS2019 to be able to generate a REST Client.

        // Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`
        app.UseSwaggerUI(c =>
        {
            var config = _configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();

            c.SwaggerEndpoint("/swagger/v1/swagger.json", WebAPITitle);
            c.RoutePrefix = "api/docs";
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        });

        app.UseHttpsRedirection();

        app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api") == false, app =>
        {
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
#pragma warning disable IDE0053 // Use expression body for lambda expressions
            app.UseEndpoints(config =>
            {
                config.MapFallbackToFile("index.html");

                // Remove until .net 7 EF support added see https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/issues/1555
                // config.MapHealthChecksUI(); // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
            });
#pragma warning restore IDE0053 // Use expression body for lambda expressions
        });
        app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/api"), api =>
        {
            api.UseStaticFiles();
            api.UseRouting();
            api.UseEndpoints(config =>
            {
                config.MapRazorPages();
                config.MapControllers();

                // Remove until .net 7 EF support added see https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks/issues/1555
                //config.MapHealthChecks(HealthCheckAPI, new HealthCheckOptions
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
            });
        });
    }
}
