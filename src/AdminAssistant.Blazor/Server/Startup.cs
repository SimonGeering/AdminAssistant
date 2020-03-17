using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdminAssistant.Blazor.Server
{
    public class Startup
    {
        // TODO: Make version a constant shared with WebAPI Assemblies
        private const string WebAPIVersion = "v1";
        private string WebAPITitle => $"Admin Assistant API {WebAPIVersion}";

        private readonly IWebHostEnvironment env;
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.env = env;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            if (this.env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(WebAPIVersion, new OpenApiInfo { Title = WebAPITitle, Version = WebAPIVersion }); // Add OpenAPI/Swagger middleware
                    c.AddFluentValidationRules(); // Adds fluent validation rules to swagger See: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.docs.xml"));
                });
            }
            services.AddAdminAssistantServerSideDomainModel();
            services.AddAdminAssistantServerSideDAL(this.configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();

                // Add OpenAPI/Swagger middleware ...
                app.UseSwagger(); // Serves the registered OpenAPI/Swagger documents on `/swagger/v1/swagger.json`
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", this.WebAPITitle)); // Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`
            }

            app.UseStaticFiles();

            app.UseClientSideBlazorFiles<Client.Program>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Program>("index.html");
            });
        }
    }
}
