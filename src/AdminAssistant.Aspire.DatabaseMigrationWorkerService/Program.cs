using AdminAssistant.Infrastructure.EntityFramework;

namespace AdminAssistant.Aspire.DatabaseMigrationWorkerService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.AddAdminAssistantApplicationDbContext();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}
