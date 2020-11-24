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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU1ODIzQDMxMzgyZTMzMmUzMEFveEs3YUcrWmdjUnJRQi80Vzd0WUZQalVRcjA3T09RMHB3ZW55dHcxRnM9;MzU1ODI0QDMxMzgyZTMzMmUzME04V1UxYWo3WW0rQ2VYMmQrTXpqemZydjQvdTVkZlUvMnBBR2cxM1F0cEk9;MzU1ODI1QDMxMzgyZTMzMmUzMG5WeFBVZHJua1hZZFhid05NZWliMnJRTHVHbkJjSENTMS9odWkyeGR6VXM9;MzU1ODI2QDMxMzgyZTMzMmUzMEZ6cDFJUnFPZENId2FqUE92cERNemN1Z1RhWmRqcWxvUGZoeS93ZHZKWWc9;MzU1ODI3QDMxMzgyZTMzMmUzMEpjbjQ3anZlRU11aDczNkNCSWF6OVhlVkJFaVJ1TjVWNnBRQXVsYzEwazQ9;MzU1ODI4QDMxMzgyZTMzMmUzMEJwWXVaSmszTFFYMFpNUnA0MDdEem5aMU5QdFhoaEJXQVd2b0FsVE4vZGM9;MzU1ODI5QDMxMzgyZTMzMmUzMFMvYnErUnFtb2dDWEpucG92R1dGU2RDaVA2TEhhdkNkR1lzMThyLzVIbm89;MzU1ODMwQDMxMzgyZTMzMmUzMFZ6cTRTZTh1cVZYcGNFZWNaZE9yaHhNRndldUt1THljZFJlejlLRkVva1k9;MzU1ODMxQDMxMzgyZTMzMmUzMEc1OUNiNHJhUVNVMUkzS0xNTWpINjdjNFdQbHRkZTBXNVYzblJzVFRwTG89;MzU1ODMyQDMxMzgyZTMzMmUzME14YkN4YU1PYkVoU1Z2QVFoRVZ1bEZwVkJuOXkwMnBQOW11azRud3BDaEE9");

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
