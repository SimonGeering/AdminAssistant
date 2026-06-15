using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AdminAssistant.AvaloniaApp.ViewModels;
using AdminAssistant.AvaloniaApp.Views;

namespace AdminAssistant.AvaloniaApp;

public partial class App : Application
{
    public override void Initialize()
        => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(), };

        base.OnFrameworkInitializationCompleted();
    }
}
