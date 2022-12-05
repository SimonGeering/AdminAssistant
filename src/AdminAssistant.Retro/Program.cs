using System.Windows;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Terminal.Gui;

using var host = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAdminAssistantWebAPIClient(new Uri("https://localhost:5001"));

        services.AddValidatorsFromAssemblyContaining<AdminAssistant.Infra.DAL.IDatabasePersistable>();

        services.AddAdminAssistantClientSideProviders();
        services.AddAdminAssistantClientSideDomainModel();
        services.AddAdminAssistantUI();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
#if DEBUG
        logging.AddConsole();
        logging.AddDebug();

        logging.AddFilter("Default", LogLevel.Information)
                .AddFilter(AdminAssistant.Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
        logging.AddFilter("Default", LogLevel.Warning)
                .AddFilter(AdminAssistant.Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

        // TODO: Configure production logging.
#endif
    }).Build();

using (var scope = host.Services.CreateScope())
{
    Label lblStatus;
    Toplevel Top;
    Window Win;
    Application.Init();

    Top = Application.Top;

    Win = new Window("Admin Assistant")
    {
        X = 0,
        Y = 1, // menu
        Width = Dim.Fill(),
        Height = Dim.Fill(1), // status bar
        ColorScheme = Colors.Base,
    };
    Top.Add(Win);
    Top.LayoutSubviews();

    var menu = new MenuBar(new MenuBarItem[]
    {
        new MenuBarItem ("_File", new MenuItem []
        {
            new MenuItem ("_Quit", "", () => Quit()),
        })
    });
    Top.Add(menu);

    var statusBar = new StatusBar(new StatusItem[]
    {
        new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => Quit()),
    });

    Win.Add(lblStatus = new Label("Len:")
    {
        Y = Pos.Bottom(Win),
        Width = Dim.Fill(),
        TextAlignment = TextAlignment.Right
    });
    Top.Add(statusBar);

    Application.Run(Top); // Must explicit call Application.Shutdown method to shutdown.
    Application.Shutdown();
}

static void Quit() => Application.RequestStop();
