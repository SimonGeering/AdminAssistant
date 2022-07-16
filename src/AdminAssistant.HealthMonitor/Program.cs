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
    setup.AddHealthCheckEndpoint("(Gateway) Admin API", "https://adminassistant.gateway:443/api/admin/health");
    setup.AddHealthCheckEndpoint("(Gateway) AssetRegister API", "https://adminassistant.gateway:443/api/assetregister/health");
    setup.AddHealthCheckEndpoint("(Gateway) Billing API", "https://adminassistant.gateway:443/api/billing/health");
    setup.AddHealthCheckEndpoint("(Gateway) Budget API", "https://adminassistant.gateway:443/api/budget/health");
    setup.AddHealthCheckEndpoint("(Gateway) Calendar API", "https://adminassistant.gateway:443/api/calendar/health");
    setup.AddHealthCheckEndpoint("(Gateway) Contacts API", "https://adminassistant.gateway:443/api/contacts/health");
    setup.AddHealthCheckEndpoint("(Gateway) Core API", "https://adminassistant.gateway:443/api/core/health");
    setup.AddHealthCheckEndpoint("(Gateway) Dashboard API", "https://adminassistant.gateway:443/api/dashboard/health");
    setup.AddHealthCheckEndpoint("(Gateway) Documents API", "https://adminassistant.gateway:443/api/documents/health");
    setup.AddHealthCheckEndpoint("(Gateway) Groceries API", "https://adminassistant.gateway:443/api/groceries/health");
    setup.AddHealthCheckEndpoint("(Gateway) Mail API", "https://adminassistant.gateway:443/api/mail/health");
    setup.AddHealthCheckEndpoint("(Gateway) Meals API", "https://adminassistant.gateway:443/api/meals/health");
    setup.AddHealthCheckEndpoint("(Gateway) Notes API", "https://adminassistant.gateway:443/api/notes/health");
    setup.AddHealthCheckEndpoint("(Gateway) Reports API", "https://adminassistant.gateway:443/api/reports/health");
    setup.AddHealthCheckEndpoint("(Gateway) Tasks API", "https://adminassistant.gateway:443/api/tasks/health");

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
