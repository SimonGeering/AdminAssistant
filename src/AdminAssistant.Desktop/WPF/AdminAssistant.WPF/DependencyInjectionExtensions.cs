using AdminAssistant.WPF;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantDesktopServices(this IServiceCollection services)
        {
            services.AddFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();

            services.AddSingleton<MainWindow>();
        }
    }
}
