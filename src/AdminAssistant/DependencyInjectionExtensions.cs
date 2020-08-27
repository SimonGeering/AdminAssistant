using AdminAssistant.Framework.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantClientSideProviders(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ClientSideLoggingProvider>();

            AddSharedProviders(services);
        }

        public static void AddAdminAssistantServerSideProviders(this IServiceCollection services)
        {
            services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();

            AddSharedProviders(services);
        }

        private static void AddSharedProviders(this IServiceCollection services)
            => services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }
}

