using AdminAssistant.UI.Modules.Accounts.BankAccountBalanceList;
using AdminAssistant.UI.Modules.Accounts.BankAccountEditDialog;
using AdminAssistant.UI.Modules.Accounts.BankAccountRightSidebar;
using AdminAssistant.UI.Modules.Accounts.BankAccountTransactionList;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.UI.Modules.Accounts
{
    public static class AccountsUIModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAccountsViewModel, AccountsViewModel>();
            services.AddTransient<IBankAccountBalanceListViewModel, BankAccountBalanceListViewModel>();
            services.AddTransient<IBankAccountEditDialogViewModel, BankAccountEditDialogViewModel>();
            services.AddTransient<IBankAccountRightSidebarViewModel, BankAccountRightSidebarViewModel>();
            services.AddTransient<IBankAccountTransactionListViewModel, BankAccountTransactionListViewModel>();
        }
    }
}
