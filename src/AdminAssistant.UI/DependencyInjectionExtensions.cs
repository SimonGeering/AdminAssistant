using AdminAssistant.Core.UI.Services;
using AdminAssistant.Core.UI.Shared.Breadcrumb;
using AdminAssistant.Core.UI.Shared.Header;
using AdminAssistant.Core.UI.Shared.Footer;
using AdminAssistant.Core.UI.Shared.Sidebar;

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
