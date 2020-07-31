using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountBalanceList;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountRightSidebar;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList;
using AdminAssistant.UI.Modules.CoreModule;
using AdminAssistant.UI.Services;
using AdminAssistant.UI.Shared.Breadcrumb;
using AdminAssistant.UI.Shared.Header;
using AdminAssistant.UI.Shared.Footer;
using AdminAssistant.UI.Shared.Sidebar;
using AdminAssistant.UI.Shared;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantUI(this IServiceCollection services)
        {
            // Add Accounts UI ...
            services.AddTransient<IAccountsViewModel, AccountsViewModel>();
            services.AddTransient<IBankAccountBalanceListViewModel, BankAccountBalanceListViewModel>();
            services.AddTransient<IBankAccountEditDialogViewModel, BankAccountEditDialogViewModel>();
            services.AddTransient<IBankAccountRightSidebarViewModel, BankAccountRightSidebarViewModel>();
            services.AddTransient<IBankAccountTransactionListViewModel, BankAccountTransactionListViewModel>();
            services.AddTransient<IAccountsService, AccountsService>();
            services.AddScoped<IAccountsStateStore, AccountsStateStore>();

            // Add Core Services ...
            services.AddTransient<ICoreService, CoreService>();

            // Add Shared UI ...
            services.AddTransient<IBreadcrumbViewModel, BreadcrumbViewModel>();
            services.AddTransient<IHeaderViewModel, HeaderViewModel>();
            services.AddTransient<IFooterViewModel, FooterViewModel>();
            services.AddTransient<ISidebarViewModel, SidebarViewModel>();

            services.AddTransient<IAppService, AppService>();
            services.AddScoped<IAppStateStore, AppStateStore>();

            services.AddTransient<ILoadingSpinner, LoadingSpinner>();
        }
    }
}
