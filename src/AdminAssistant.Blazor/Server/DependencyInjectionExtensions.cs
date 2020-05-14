using AdminAssistant.DomainModel.Infrastructure;
using Ardalis.GuardClauses;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantServerServices(this IServiceCollection services, ConfigurationSettings configurationSettings)
        {
            Guard.Against.Null(configurationSettings, nameof(configurationSettings));

            services.AddFrameworkServices();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddAdminAssistantServerSideDAL(configurationSettings);
        }
    }
}
