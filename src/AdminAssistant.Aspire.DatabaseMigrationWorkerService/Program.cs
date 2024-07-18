namespace AdminAssistant.Aspire.DatabaseMigrationWorkerService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}