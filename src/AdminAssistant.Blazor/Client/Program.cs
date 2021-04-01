using Syncfusion.Blazor;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Logging;
using FluentValidation;

namespace AdminAssistant.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIxOTM1QDMxMzkyZTMxMmUzMGJ6Y3AzVVhlMHlRbUFEUDNnZk1HT2F3VThtZ282MU84QjVab0Q1SG5VQ2c9;NDIxOTM2QDMxMzkyZTMxMmUzMFZ0MXhYZ2RPaGdCME9XRnVHUU5jeWxMMVpFWUJNYVFmMGU5eEZiS0wrMzg9;NDIxOTM3QDMxMzkyZTMxMmUzMFpHWXlCbFRKNVg0M3R4OE1FZUVSOW4vUkxnc2gvUTB1UnFxME9DaGNKWkU9;NDIxOTM4QDMxMzkyZTMxMmUzMEJ5M2JZNTZEbDdjMi9iMXpmT2F5R0J0OTBKVXpHRDRzZEh6ZzhRbmVhWTA9;NDIxOTM5QDMxMzkyZTMxMmUzMGtHSVdTSmsveGcxNHFLOHNkVVZwUy8vMU9kQUNqNzNTcnZ3ZkplQmk1UzQ9;NDIxOTQwQDMxMzkyZTMxMmUzMGFlY29pOXdsMXl1TVBoZlJBWlY1N0ZaZEVPUVhabGJuUCtDcThGQi9ueDg9;NDIxOTQxQDMxMzkyZTMxMmUzMG9rbXQ1UG4yTVpXWW13OTA5eHhOZHZzcE5sdElSYTlDMnJHSi9tbzJheWs9;NDIxOTQyQDMxMzkyZTMxMmUzMFpaMzBtM1FJL3FtcWpaNlFIK05QN2NOMTRIalN1WkhGdkhKZW1Cc2Y3QkE9;NDIxOTQzQDMxMzkyZTMxMmUzMFJiUFRBeHBrL2l1MFp4di9FbUZ2UnBUcE1XWTF1MSt3OWNWc0NPQzV5dVE9;NDIxOTQ0QDMxMzkyZTMxMmUzMEtGcmczQUtXUUUzdTliYXpzUWZReGJoMHY0WGRaUUh2WXlTV1dxT3JQRlU9");
            // V18.4.0.30 - Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY3NTcxQDMxMzgyZTM0MmUzMEVOQ2lHSkozUytnZ3lqUGZka1QxeXdsZ1BGRlFxSTljcmtQU3N1NlpVc2M9;MzY3NTcyQDMxMzgyZTM0MmUzMGVtK0Z3MWR3Q1FiaW16aWx1R1pWZlRFdkZTYllaWkRCVm1mSGZjSndRZkU9;MzY3NTczQDMxMzgyZTM0MmUzMGF5ZlU0cjczVFZjbGhPTDVHdklvemx2YllEMHN6QUhtNE02Z0xkclZJdms9;MzY3NTc0QDMxMzgyZTM0MmUzMGx2UTJZSFE2SU1XUExUSVl3MFFsUkJZQmdWYnUva1NUdjM2ckZaRFBiSm89;MzY3NTc1QDMxMzgyZTM0MmUzMEpHTEdwKzFKWHZNTFhDZTFEUG5hNHAxc1pYOG9aN1p6UEdvWGNmcWdZNnM9;MzY3NTc2QDMxMzgyZTM0MmUzMEY1ejlNYmYxSEJrRnZOOG5UaUJpdmZJNE5SaGlXRWFKWHdtRG5GZ2Y1azA9;MzY3NTc3QDMxMzgyZTM0MmUzMGYyS0VpakR5QWVTa1o1WjFIdGNKbGQ3VGxWVW9qSGcxNkcycTEwampZTHM9;MzY3NTc4QDMxMzgyZTM0MmUzMG5ZelkwUTQ4WFgwRk9tcDdMNEhpNjBYdkZKcnNNVzd3ZW1aOHVuRTMybVE9;MzY3NTc5QDMxMzgyZTM0MmUzMFppeWcyVTMyZlMyVTFyRHB2YUp5WHEyMW9lWnFVQnFrSDIzY0I2UFFxcHc9;MzY3NTgwQDMxMzgyZTM0MmUzMGU2bU9uV21OcGJkNHZQdisvbjlGckExUUVYVUxtbjlOOXJJOXBjV05pY1E9");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddAdminAssistantWebAPIClient(new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddLogging(logging =>
            {
                // NB Configuration must be done in code as no other option is currently supported client side.
                // See: https://github.com/BlazorExtensions/Logging
                logging.ClearProviders();
                logging.AddBrowserConsole();
#if DEBUG
                logging.AddFilter("Default", LogLevel.Information)
                       .AddFilter(Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
                       .AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
                logging.AddFilter("Default", LogLevel.Warning)
                       .AddFilter(Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Warning)
                       .AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

                // TODO: Configure other production logging options.
#endif
            });

            // See https://github.com/ryanelian/FluentValidation.Blazor
            builder.Services.AddValidatorsFromAssemblyContaining<Infra.DAL.IDatabasePersistable>();

            // See https://blazor.syncfusion.com/documentation/introduction/
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddAdminAssistantClientSideProviders();
            builder.Services.AddAdminAssistantClientSideDomainModel();
            builder.Services.AddAdminAssistantUI();

            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
