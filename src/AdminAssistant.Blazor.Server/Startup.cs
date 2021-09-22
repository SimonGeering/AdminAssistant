using AdminAssistant.DomainModel.Shared;
using Ardalis.GuardClauses;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace AdminAssistant.Blazor.Server;

public class Startup
{
    // TODO: Make version a constant shared with WebAPI Assemblies
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
        var config = _configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();

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
                c.AddFluentValidationRules(); // Adds fluent validation rules to swagger schema See: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation

                // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
                c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations
            });
        services.AddSwaggerGenNewtonsoftSupport();

        services.AddAutoMapper(typeof(Infra.DAL.MappingProfile), typeof(WebAPI.v1.MappingProfile));

        services.AddAdminAssistantServerSideProviders();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantServerSideInfra(config);
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
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseHealthChecks("/api/health");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
