using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AdminModule;
using AdminAssistant.UI.Modules.AssetRegisterModule;
using AdminAssistant.UI.Modules.BillingModule;
using AdminAssistant.UI.Modules.BudgetModule;
using AdminAssistant.UI.Modules.CalendarModule;
using AdminAssistant.UI.Modules.ContactsModule;
using AdminAssistant.UI.Modules.CoreModule;
using AdminAssistant.UI.Modules.DashboardModule;
using AdminAssistant.UI.Modules.DocumentsModule;
using AdminAssistant.UI.Modules.MailModule;
using AdminAssistant.UI.Modules.ReportsModule;
using AdminAssistant.UI.Modules.TasksModule;
using AdminAssistant.UI.Shared;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantWebAPIClient(this IServiceCollection services, System.Uri baseAddress)
        {
            services.AddHttpClient<IAdminAssistantWebAPIClient, AdminAssistantWebAPIClient>(AdminAssistant.Constants.AdminAssistantWebAPIClient, (httpClient) => httpClient.BaseAddress = baseAddress);
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void AddAdminAssistantUI(this IServiceCollection services)
        {
            // Add MVVM Toolkit registrations ...
            services.AddSingleton<IMessenger, StrongReferenceMessenger>();

            // Add Accounts UI ...
            services.AddSingleton<IAccountsViewModel, AccountsViewModel>();

            services.AddSingleton<IBankAccountBalanceListViewModel, BankAccountBalanceListViewModel>();
            services.AddSingleton<IBankAccountEditDialogViewModel, BankAccountEditDialogViewModel>();
            services.AddSingleton<IBankAccountRightSidebarViewModel, BankAccountRightSidebarViewModel>();
            services.AddSingleton<IBankAccountTransactionListViewModel, BankAccountTransactionListViewModel>();

            services.AddTransient<IAccountsService, AccountsService>();

            // Add Admin UI ...
            services.AddSingleton<IAdminViewModel, AdminViewModel>();

            // Add Asset Register UI ...
            services.AddSingleton<IAssetRegisterViewModel, AssetRegisterViewModel>();

            // Add Billing UI ...
            services.AddSingleton<IBillingViewModel, BillingViewModel>();

            // Add Budget UI ...
            services.AddSingleton<IBudgetViewModel, BudgetViewModel>();

            // Add Calendar UI ...
            services.AddSingleton<ICalendarViewModel, CalendarViewModel>();

            // Add Contacts UI ...
            services.AddSingleton<IContactsViewModel, ContactsViewModel>();

            // Add Core UI ...
            services.AddTransient<ICoreService, CoreService>();

            // Add Dashboard UI ...
            services.AddSingleton<IDashboardViewModel, DashboardViewModel>();

            // Add Documents UI ...
            services.AddSingleton<IDocumentsViewModel, DocumentsViewModel>();
            services.AddTransient<IDocumentsService, DocumentsService>();

            // Add Mail UI ...
            services.AddSingleton<IMailViewModel, MailViewModel>();

            // Add Reports UI ...
            services.AddSingleton<IReportsViewModel, ReportsViewModel>();

            // Add Tasks UI ...
            services.AddSingleton<ITasksViewModel, TasksViewModel>();

            // Add Shared UI ...
            services.AddTransient<IAppService, AppService>();
            services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
        }
    }
}
