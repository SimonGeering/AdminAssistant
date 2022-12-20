using AdminAssistant.Infra.Providers;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AdminAssistant.Test")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantClientSideProviders(this IServiceCollection services)
    {
        // TODO: For now take a hard dependency between client side UI projects and Infra, inc un-needed anciliairy dependencies.
        // This can be resolved in future by splitting infra into multiple assemblies, but this is not worth doing
        // until it gets bigger sure to implementation of other integrations.
        services.AddTransient<ILoggingProvider, ClientSideLoggingProvider>();
        services.AddSharedProviders();
    }

    public static void AddSharedProviders(this IServiceCollection services)
        => services.AddTransient<IDateTimeProvider, DateTimeProvider>();
}
