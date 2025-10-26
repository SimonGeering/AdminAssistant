using AdminAssistant.DatabaseMigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddAdminAssistantApplicationDbContext();

var host = builder.Build();
await host.RunAsync();
