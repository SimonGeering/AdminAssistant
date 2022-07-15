var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecksUI(setupSettings: setup =>
{
    // https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
    setup.AddHealthCheckEndpoint("Accounts API", "http://adminassistant.accounts.api:80/health");
    setup.AddHealthCheckEndpoint("Admin API", "http://adminassistant.admin.api:80/health");
    setup.AddHealthCheckEndpoint("AssetRegister API", "http://adminassistant.assetregister.api:80/health");
    setup.AddHealthCheckEndpoint("Billing API", "http://adminassistant.billing.api:80/health");
    setup.AddHealthCheckEndpoint("Budget API", "http://adminassistant.budget.api:80/health");
    setup.AddHealthCheckEndpoint("Calendar API", "http://adminassistant.calendar.api:80/health");
    setup.AddHealthCheckEndpoint("Contacts API", "http://adminassistant.contacts.api:80/health");
    setup.AddHealthCheckEndpoint("Core API", "http://adminassistant.core.api:80/health");
    setup.AddHealthCheckEndpoint("Dashboard API", "http://adminassistant.dashboard.api:80/health");
    setup.AddHealthCheckEndpoint("Documents API", "http://adminassistant.documents.api:80/health");
    setup.AddHealthCheckEndpoint("Groceries API", "http://adminassistant.groceries.api:80/health");
    setup.AddHealthCheckEndpoint("Mail API", "http://adminassistant.mail.api:80/health");
    setup.AddHealthCheckEndpoint("Meals API", "http://adminassistant.meals.api:80/health");
    setup.AddHealthCheckEndpoint("Notes API", "http://adminassistant.notes.api:80/health");
    setup.AddHealthCheckEndpoint("Reports API", "http://adminassistant.reports.api:80/health");
    setup.AddHealthCheckEndpoint("Tasks API", "http://adminassistant.tasks.api:80/health");
}).AddInMemoryStorage();

var app = builder.Build();

app.UseRouting()
   .UseEndpoints(config => config.MapHealthChecksUI());

app.Run();
