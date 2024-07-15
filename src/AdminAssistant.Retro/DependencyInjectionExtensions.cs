using System.Diagnostics.CodeAnalysis;
using AdminAssistant.Retro.Modules.AccountsModule;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAdminAssistantRetroUIElements(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();

        services.AddAccountsRetroUIElements();
        return services;
    }

    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "WIP")]
    private static IServiceCollection AddAccountsRetroUIElements(this IServiceCollection services)
    {
        services.AddTransient<BankAccountEditDialog>();
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>
        return services;
    }
}
