using AdminAssistant;
using AdminAssistant.Web;
using FluentValidation;
using MudBlazor.Services;
using SimonGeering.Framework.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.AddRedisOutputCache(Constants.CacheName);
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options =>
    {
        if (builder.Environment.IsDevelopment())
            options.DetailedErrors = true;
    });
builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI();
builder.Services.AddAdminAssistantApiClient();
builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new($"https+http://{Constants.Api}");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<AdminAssistant.Web.Shared.App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

await app.RunAsync();
