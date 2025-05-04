using System.Diagnostics;
using AdminAssistant.Infrastructure.EntityFramework;
using OpenTelemetry.Trace;

namespace AdminAssistant.Aspire.DatabaseMigrationWorkerService;

public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    private static readonly ActivitySource ActivitySource = new("Migrations");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.EnsureDatabaseAsync(stoppingToken);
            await dbContext.RunMigrationAsync(stoppingToken);
            await dbContext.SeedDataAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }
        hostApplicationLifetime.StopApplication();
    }
}
