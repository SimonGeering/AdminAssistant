using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantServerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFrameworkServices();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddAdminAssistantServerSideDAL(configuration);
        }
    }
}
