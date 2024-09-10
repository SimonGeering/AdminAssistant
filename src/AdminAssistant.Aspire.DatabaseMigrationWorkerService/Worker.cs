using System.Diagnostics;
using AdminAssistant.Infrastructure.EntityFramework;
using OpenTelemetry.Trace;

namespace AdminAssistant.Aspire.DatabaseMigrationWorkerService;

public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    private static readonly ActivitySource ActivitySource = new("Migrations");

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.EnsureDatabaseAsync(cancellationToken);
            await dbContext.RunMigrationAsync(cancellationToken);
            await dbContext.SeedDataAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }
        hostApplicationLifetime.StopApplication();
    }
}
