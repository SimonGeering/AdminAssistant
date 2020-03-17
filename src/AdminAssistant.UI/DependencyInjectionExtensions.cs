using AdminAssistant.UI.Services;
using AdminAssistant.UI.Shared.Breadcrumb;
using AdminAssistant.UI.Shared.Header;
using AdminAssistant.UI.Shared.Footer;
using AdminAssistant.UI.Shared.Sidebar;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCoreUI(this IServiceCollection services)
        {
            services.AddTransient<IBreadcrumbViewModel, BreadcrumbViewModel>();
            services.AddTransient<IHeaderViewModel, HeaderViewModel>();
            services.AddTransient<IFooterViewModel, FooterViewModel>();
            services.AddTransient<ISidebarViewModel, SidebarViewModel>();
            services.AddTransient<IAppService, AppService>();
            services.AddScoped<IAppStateStore, AppStateStore>();
        }
    }
}
