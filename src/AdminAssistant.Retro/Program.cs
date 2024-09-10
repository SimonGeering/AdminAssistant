using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimonGeering.Framework.Primitives;

AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
{
    #if DEBUG
        if (System.Diagnostics.Debugger.IsAttached)
            System.Diagnostics.Debug.WriteLine(e.ExceptionObject.ToString());
    #endif
    Console.WriteLine(e.ExceptionObject.ToString());
    Console.ReadLine();
};

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddAdminAssistantWebAPIClient();
builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();
builder.Services.AddAdminAssistantClientSideProviders();
builder.Services.AddAdminAssistantClientSideDomainModel();
builder.Services.AddAdminAssistantUI();
builder.Services.AddAdminAssistantRetroUIElements();

using var host = builder.Build();
using var scope = host.Services.CreateScope();

Application.Init();
Application.QuitKey = Key.Q.WithCtrl ;

var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
Application.Run(mainWindow);

mainWindow.Dispose();
Application.Shutdown();
