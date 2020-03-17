using AdminAssistant.DomainModel.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCoreInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IUserContextProvider, UserContextProvider>();
        }
    }
}

