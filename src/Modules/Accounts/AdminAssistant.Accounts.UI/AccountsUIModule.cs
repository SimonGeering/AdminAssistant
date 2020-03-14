using AdminAssistant.Accounts.UI.BankAccountBalanceList;
using AdminAssistant.Accounts.UI.BankAccountEditDialog;
using AdminAssistant.Accounts.UI.BankAccountRightSidebar;
using AdminAssistant.Accounts.UI.BankAccountTransactionList;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Accounts.UI
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
