using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Framework.Providers
{
    internal static class ProvidersModule
    {
        internal static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
