using AdminAssistant.WPF;
using AdminAssistant.WPF.Modules.Accounts;

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
            services.AddTransient<BankAccountEditDialog>();
        }
    }
}
