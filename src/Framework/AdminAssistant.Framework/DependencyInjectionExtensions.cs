using AdminAssistant.Framework.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IHttpClientJsonProvider, HttpClientJsonProvider>();
            services.AddTransient<ILoggingProvider, LoggingProvider>();
        }
    }
}

