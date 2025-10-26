using Avalonia;
using System;

namespace AdminAssistant.AvaloniaApp;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}

// Aspire and host builder research WIP
/*
https://github.com/AvaloniaUI/Avalonia/issues/5241
https://gist.github.com/carstencodes/8a151a772540fb3b3b6310caa6e79efc
https://github.com/NeverMorewd/Hosting.Avaloniaui
https://github.com/LaurentInSeattle/Lyt.Avalonia.Framework/blob/main/Lyt.Avalonia.Mvvm/ApplicationBase.cs

// Currently using code from:
https://github.com/AvaloniaUI/Avalonia.Samples/pull/64
*/
