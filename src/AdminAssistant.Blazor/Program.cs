using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.Providers;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddBlazorise(options => options.Immediate = true)
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddAdminAssistantWebAPIClient(new Uri("https://localhost/"));
builder.Services.AddLogging(logging =>
{
    // NB Configuration must be done in code as no other option is currently supported client side.
    // See: https://github.com/BlazorExtensions/Logging
    logging.ClearProviders();
    //logging.AddBrowserConsole();
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
//builder.Services.AddValidatorsFromAssemblyContaining<IDatabasePersistable>();

builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI(AdminAssistant.UI.Shared.FontAwesomeVersionEnum.V5o15o4);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
