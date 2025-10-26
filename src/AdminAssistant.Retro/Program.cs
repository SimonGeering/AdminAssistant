#pragma warning disable IDE0090 // Use 'new(...)'
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Retro.Modules.AccountsModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimonGeering.Framework.Primitives;

using var host = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddValidatorsFromAssemblyContaining<IPersistable>();
        services.AddAdminAssistantClientSideProviders();
        services.AddAdminAssistantClientSideDomainModel();
        services.AddAdminAssistantApiClient();
        services.AddAdminAssistantUI();
        services.AddAdminAssistantRetroUIElements();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
#if DEBUG
        logging.AddConsole();
        logging.AddDebug();

        logging.AddFilter("Default", LogLevel.Information)
                .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
        logging.AddFilter("Default", LogLevel.Warning)
                .AddFilter(ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
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
        }),
        new MenuBarItem ("_Accounts", new MenuItem []
        {
            new MenuItem ("_BankAccountEdit", "", () => AccountsBankAccountEdit(scope)),
        })
    });
    Top.Add(menu);

    var statusBar = new StatusBar(new StatusItem[]
    {
        new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => Quit()),
    });

    lblStatus = new Label("Len:")
    {
        Y = Pos.Bottom(Win),
        Width = Dim.Fill(),
        TextAlignment = TextAlignment.Right
    };
    Win.Add(lblStatus);
    Top.Add(statusBar);

    Application.Run(Top); // Must explicit call Application.Shutdown method to shutdown.
    Application.Shutdown();
}

static void Quit() => Application.RequestStop();
static void AccountsBankAccountEdit(IServiceScope scope)
{
    var bankAccountEditDialog = scope.ServiceProvider.GetRequiredService<BankAccountEditDialog>();
    Application.Run(bankAccountEditDialog);
}
#pragma warning restore IDE0090 // Use 'new(...)'
