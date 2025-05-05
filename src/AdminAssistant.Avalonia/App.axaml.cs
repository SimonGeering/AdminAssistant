using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AdminAssistant.Avalonia.Views;
using AdminAssistant.Shared.UI;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimonGeering.Framework.Primitives;

namespace AdminAssistant.Avalonia;

public partial class App : Application
{
    public IHost? GlobalHost { get; private set; }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override async void OnFrameworkInitializationCompleted()
    {
        var hostBuilder = CreateHostBuilder();
        var host = hostBuilder.Build();
        GlobalHost = host;

        var collection = new ServiceCollection();
        collection.AddAdminAssistantClientSideProviders();
        collection.AddAdminAssistantClientSideDomainModel();
        collection.AddAdminAssistantUI();

        var services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = GlobalHost.Services.GetRequiredService<IMainWindowViewModel>(),
            };
            desktop.Exit += (sender, args) =>
            {
                GlobalHost.StopAsync(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
                GlobalHost.Dispose();
                GlobalHost = null;
            };
        }

        DataTemplates.Add(GlobalHost.Services.GetRequiredService<ViewLocator>());

        base.OnFrameworkInitializationCompleted();

        // Usually, we don't want to block main UI thread.
        // But if it's required to start async services before we create any window,
        // then don't set any MainWindow, and simply call Show() on a new window later after async initialization.
        await host.StartAsync();
    }


    private static HostApplicationBuilder CreateHostBuilder()
    {
        // Alternatively, we can use Host.CreateDefaultBuilder, but this sample focuses on HostApplicationBuilder.
        var builder = Host.CreateApplicationBuilder(Environment.GetCommandLineArgs());

        builder.AddServiceDefaults();

        builder.Services.AddAdminAssistantWebAPIClient();
        builder.Services.AddValidatorsFromAssemblyContaining<IPersistable>();
        builder.Services.AddAdminAssistantClientSideProviders();
        builder.Services.AddAdminAssistantClientSideDomainModel();
        builder.Services.AddAdminAssistantUI();
        //builder.Services.AddAdminAssistantViews();

        // builder.Services.AddOptions<WeatherSettings>().Bind(builder.Configuration.GetSection("Weather"));
        // builder.Services.AddHostedService<HostedBackgroundService>();

        // builder.Services.AddTransient<IWeatherService, WeatherService>();
        builder.Services.AddTransient<ViewLocator>();
        // builder.Services.AddTransient<MainWindowViewModel>();

        // builder.Services.AddView<DayReportViewModel, DayReportView>();
        return builder;
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
