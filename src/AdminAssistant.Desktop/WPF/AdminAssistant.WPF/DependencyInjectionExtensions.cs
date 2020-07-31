using AdminAssistant.WPF;
using AdminAssistant.WPF.Modules.AccountsModule;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantDesktopServices(this IServiceCollection services)
        {
            services.AddClientFrameworkServices();
            services.AddAdminAssistantClientSideDomainModel();
            services.AddAdminAssistantUI();

            services.AddSingleton<MainWindow>();
            services.AddTransient<BankAccountEditDialog>();
        }
    }
}
