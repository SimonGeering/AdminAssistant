using AdminAssistant.DomainModel.Shared;
using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace AdminAssistant.Blazor.Server
{
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
            var configSettings = _configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.Authority = $"https://{configSettings.Auth0Authority}/";
                opts.Audience = configSettings.Auth0ApiIdentifier;
            });

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

            services.AddHttpContextAccessor();
            services.AddControllers(opts =>
            {
                // Add [Authorize] for all controllers ...
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opts.Filters.Add(new AuthorizeFilter(policy));

            }).AddNewtonsoftJson()
              .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Infra.DAL.IDatabasePersistable>());

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "Open Id" }
                            },
                            AuthorizationUrl = new System.Uri($"https://{configSettings.Auth0Authority}/" + "authorize?audience=" + configSettings.Auth0ApiIdentifier)
                        }
                    }
                });
                c.AddFluentValidationRules(); // Adds fluent validation rules to swagger schema See: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation

                // Include documentation from Annotations (Swashbuckle.AspNetCore.Annotations)...
                c.EnableAnnotations(); // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#install-and-enable-annotations

                // Let Swagger UI know that we put Authorize on all endpoints using a filter policy
                // See services.AddControllers code above ...
                c.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();
            });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", WebAPITitle);
                c.RoutePrefix = "api-docs";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                // Additional OAuth settings (See https://github.com/swagger-api/swagger-ui/blob/v3.10.0/docs/usage/oauth2.md)
                c.OAuthClientId(_configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>().Auth0ClientId);
            });
            
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }

    public class SwaggerSecurityRequirementsOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        /// <summary>
        /// Applies this filter on swagger documentation generation.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            // then check if there is a method-level 'AllowAnonymous', as this overrides any controller-level 'Authorize'
            var anonControllerScope = context
                    .MethodInfo
                    .DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>();

            var anonMethodScope = context
                    .MethodInfo
                    .GetCustomAttributes(true)
                    .OfType<AllowAnonymousAttribute>();

            // only add authorization specification information if there is at least one 'Authorize' in the chain and NO method-level 'AllowAnonymous'
            if (!anonMethodScope.Any() && !anonControllerScope.Any())
            {
                // add generic message if the controller methods dont already specify the response type
                if (!operation.Responses.ContainsKey("401"))
                    operation.Responses.Add("401", new OpenApiResponse { Description = "If Authorization header not present, has no value or no valid jwt bearer token" });

                if (!operation.Responses.ContainsKey("403"))
                    operation.Responses.Add("403", new OpenApiResponse { Description = "If user not authorized to perform requested action" });

                var jwtAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ jwtAuthScheme ] = new List<string>()
                    }
                };
            }
        }
    }
}
