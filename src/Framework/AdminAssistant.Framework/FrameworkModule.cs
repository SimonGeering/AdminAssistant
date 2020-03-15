using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Framework
{
    public static class FrameworkModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Providers.ProvidersModule.ConfigureServices(services);
        }
    }
}
