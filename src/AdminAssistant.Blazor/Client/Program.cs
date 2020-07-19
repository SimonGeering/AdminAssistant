using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Logging;
using FluentValidation;
using AutoMapper;

namespace AdminAssistant.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg5MjM3QDMxMzgyZTMyMmUzMGRjVW1wckY3MHc5eFB2Qkc4VWRCZnNOYVBrZGZxVGZQOXZ5WFV0LzA2OVk9;Mjg5MjM4QDMxMzgyZTMyMmUzMFF0Vi8vb0ZFcW5KdmdsS2lpMlkxK3I4OE9RVVRBZi9tSlVLc0JrZTFiRlE9;Mjg5MjM5QDMxMzgyZTMyMmUzMFBCQlV4Q2Nwa0RKY0p5dUQwVHRZWkswUEk0ZTNGd3o2RXBrRm1TUGc3TFU9;Mjg5MjQwQDMxMzgyZTMyMmUzMFpoTXc5QmcwZlQySWwzSDJxRVVjcFdHSjBOV05QcE1pMUZCck9XSDM3MWM9;Mjg5MjQxQDMxMzgyZTMyMmUzMFU0QlFtSU5ZMTZrTTNweS9hZEdOcmNmVkFweC9wcUl6cjYxNVhRMWRVVlU9;Mjg5MjQyQDMxMzgyZTMyMmUzMEJvbTNxQ1ZweUtxSXQ3STNnNzZ2cDRDRTlSWTk0S04vaGtKZ00vM0J5cnM9;Mjg5MjQzQDMxMzgyZTMyMmUzMFRZc2hXeU9Ld1FmQXgveXZ2NEl2bzhpcVY1cjFFK1JRUzZDM3Z0Njh2RG89;Mjg5MjQ0QDMxMzgyZTMyMmUzMGllRFdtWHJ2QjIya3liYXUwbGIra2ZTSmdlNG0yZFNEQWtaQXZwT2YrQ3c9;Mjg5MjQ1QDMxMzgyZTMyMmUzMFpXSEdtV1huOXJvLzBUUWpRSWYzcWhFZUcvcVFJdFhHVml3QytqbWkvOHM9;NT8mJyc2IWhia31hfWN9Z2ZoYmF8YGJ8ampqanNiYmlmamlmanMDHmggOj48PRMwPD4jPCY9Nz88NDowfTA8fSY4;Mjg5MjQ2QDMxMzgyZTMyMmUzMEJrOUxhODlxR2Fva3hkWnkweElUYjVld1lTUDlMTEFWaUNiaWxIYVdNdGc9");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLogging(logging =>
            {
                // NB Configuration must be done in code as no other option is currently supported client side.
                // See: https://github.com/BlazorExtensions/Logging
                logging.ClearProviders();
                logging.AddBrowserConsole();
#if DEBUG
                logging.AddFilter("Default", LogLevel.Information)
                    .AddFilter(Framework.Providers.LoggingProvider.LogCategoryName, LogLevel.Debug)
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
                logging.AddFilter("Default", LogLevel.Warning)
                    .AddFilter(Framework.Providers.LoggingProvider.LogCategoryName, LogLevel.Warning)
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

                // TODO: Configure other production logging options.
#endif
            });
            builder.Services.AddAutoMapper(typeof(WebAPI.MappingProfile));

            // See https://github.com/ryanelian/FluentValidation.Blazor
            builder.Services.AddValidatorsFromAssemblyContaining<DomainModel.Modules.AccountsModule.Validation.BankAccountValidator>();

            // See https://blazor.syncfusion.com/documentation/introduction/
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddAdminAssistantClientServices();

            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
