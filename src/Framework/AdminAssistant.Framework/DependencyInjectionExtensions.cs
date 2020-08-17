using AdminAssistant.Framework.MediatR;
using AdminAssistant.Framework.Providers;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddClientFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ClientSideLoggingProvider>();

            AddFrameworkServices(services);
        }

        public static void AddServerFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

            AddFrameworkServices(services);
        }

        private static void AddFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}

