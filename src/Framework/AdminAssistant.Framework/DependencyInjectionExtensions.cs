using AdminAssistant.Framework.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddClientFrameworkServices(this IServiceCollection services)
        {
            AddFrameworkServices(services);

            services.AddTransient<IHttpClientProvider, HttpClientProvider>();
        }

        public static void AddServerFrameworkServices(this IServiceCollection services)
        {
            AddFrameworkServices(services);
        }

        private static void AddFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<ILoggingProvider, LoggingProvider>();
        }
    }
}

