using MudBlazor.Services;
using AdminAssistant.Infrastructure.Providers;
using Microsoft.AspNetCore.Components.Web;
using SimonGeering.Framework.Primitives;
using AdminAssistant.ServerSideBlazor;
using AdminAssistant.Blazor.Client;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAdminAssistantWebAPIClient();

// See https://github.com/ryanelian/FluentValidation.Blazor
builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();

builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddMudServices();
//builder.Services.AddAdminAssistantWebAPIClient();
//builder.Services.AddLogging(logging =>
//{
//    // NB Configuration must be done in code as no other option is currently supported client side.
//    // See: https://github.com/BlazorExtensions/Logging
//#if DEBUG
//    logging.AddFilter("Default", LogLevel.Information)
//            .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
//            .AddFilter("Microsoft", LogLevel.Warning)
//            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
//#else
//    logging.AddFilter("Default", LogLevel.Warning)
//            .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Warning)
//            .AddFilter("Microsoft", LogLevel.Warning)
//            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

//    // TODO: Configure other production logging options.
//#endif
//});

//// See https://github.com/ryanelian/FluentValidation.Blazor
//builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();

//builder.Services.AddAdminAssistantClientSideProviders();
//builder.Services.AddAdminAssistantClientSideDomainModel();
//builder.Services.AddAdminAssistantUI();

//await builder.Build().RunAsync();
