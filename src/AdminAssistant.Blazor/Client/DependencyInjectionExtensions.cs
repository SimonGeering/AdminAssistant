namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantClientServices(this IServiceCollection services)
        {
            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();
        }
    }
}
