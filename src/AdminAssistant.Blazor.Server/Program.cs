using AdminAssistant.Blazor.Server;
using AdminAssistant.Infra.Providers;

Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
#if DEBUG
        logging.AddConsole();
        logging.AddDebug();

        logging.AddFilter("Default", LogLevel.Information)
                .AddFilter(ILoggingProvider.ServerSideLogCategory, LogLevel.Debug)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
        logging.AddFilter("Default", LogLevel.Warning)
                .AddFilter(ILoggingProvider.ServerSideLogCategory, LogLevel.Warning)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

        // TODO: Configure production logging.
#endif
    })
    .ConfigureAppConfiguration((hostingContext, config) => config.AddCommandLine(args))
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .Build()
    .Run();
