using AdminAssistant.Framework.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddClientSideProviders(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ClientSideLoggingProvider>();

            AddSharedProviders(services);
        }

        public static void AddServerSideProviders(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();

            AddSharedProviders(services);
        }

        private static void AddSharedProviders(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}

