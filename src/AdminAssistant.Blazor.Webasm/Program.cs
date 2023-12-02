using AdminAssistant.Blazor.Client;
using AdminAssistant.Infra.Providers;
using MudBlazor.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using SimonGeering.Framework.Primitives;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddAdminAssistantWebAPIClient(new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddLogging(logging =>
{
    // NB Configuration must be done in code as no other option is currently supported client side.
    // See: https://github.com/BlazorExtensions/Logging
#if DEBUG
    logging.AddFilter("Default", LogLevel.Information)
            .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
    logging.AddFilter("Default", LogLevel.Warning)
            .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Warning)
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

    // TODO: Configure other production logging options.
#endif
});

// See https://github.com/ryanelian/FluentValidation.Blazor
builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();

builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI();

await builder.Build().RunAsync();
