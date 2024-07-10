#pragma warning disable IDE0090 // Use 'new(...)'
using AdminAssistant.Retro.Modules.AccountsModule;
using AdminAssistant.Shared;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimonGeering.Framework.Primitives;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

var configSettings = builder.Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>();
Guard.Against.Null(configSettings, nameof(configSettings), "Failed to load configuration settings");

builder.Services.AddAdminAssistantWebAPIClient(configSettings);
builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();
builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI();

builder.Services.AddAdminAssistantRetroUIElements();

using var host = new HostBuilder().Build();

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
