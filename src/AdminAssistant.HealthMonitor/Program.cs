var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    setup.UseApiEndpointHttpMessageHandler(serviceProvider => new HttpClientHandler
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
    });

    // Main API Gateway Access
    setup.AddHealthCheckEndpoint("(Gateway) Accounts API", "https://adminassistant.gateway:443/api/accounts/health");

    if (builder.Environment.IsDevelopment())
    {
        // Debug & swagger Access 
        setup.AddHealthCheckEndpoint("(Debugging) Accounts API", "https://adminassistant.accounts.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Admin API", "https://adminassistant.admin.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) AssetRegister API", "https://adminassistant.assetregister.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Billing API", "https://adminassistant.billing.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Budget API", "https://adminassistant.budget.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Calendar API", "https://adminassistant.calendar.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Contacts API", "https://adminassistant.contacts.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Core API", "https://adminassistant.core.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Dashboard API", "https://adminassistant.dashboard.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Documents API", "https://adminassistant.documents.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Groceries API", "https://adminassistant.groceries.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Mail API", "https://adminassistant.mail.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Meals API", "https://adminassistant.meals.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Notes API", "https://adminassistant.notes.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Reports API", "https://adminassistant.reports.api:443/health");
        setup.AddHealthCheckEndpoint("(Debugging) Tasks API", "https://adminassistant.tasks.api:443/health");
    }

}).AddInMemoryStorage();

var app = builder.Build();

app.UseRouting()
   .UseEndpoints(config => config.MapHealthChecksUI());

app.Run();
