using AdminAssistant.Modules.ContactsModule.UI;
using AdminAssistant.Modules.NotesModule.UI;
using AdminAssistant.Shared;
using AdminAssistant.Modules.ReportsModule.UI;
using AdminAssistant.Modules.TasksModule.UI;
using AdminAssistant.Shared.UI;
using AdminAssistant.Modules.AccountsModule.UI;
using AdminAssistant.Modules.AccountsModule.AdminUI;
using AdminAssistant.Modules.AdminModule.AdminUI;
using AdminAssistant.Modules.AssetRegisterModule.UI;
using AdminAssistant.Modules.BillingModule.UI;
using AdminAssistant.Modules.BudgetModule.UI;
using AdminAssistant.Modules.CalendarModule.UI;
using AdminAssistant.Modules.CoreModule.AdminUI;
using AdminAssistant.Modules.CoreModule.UI;
using AdminAssistant.Modules.DashboardModule.UI;
using AdminAssistant.Modules.DocumentsModule.UI;
using AdminAssistant.Modules.MailModule.UI;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantWebAPIClient(this IServiceCollection services, ConfigurationSettings configurationSettings)
        => AddAdminAssistantWebAPIClient(services, new Uri(configurationSettings.WebApiClientBaseAddress));

    public static void AddAdminAssistantWebAPIClient(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<IAdminAssistantWebAPIClient, AdminAssistantWebAPIClient>(AdminAssistant.Constants.AdminAssistantWebAPIClient, (httpClient) => httpClient.BaseAddress = baseAddress);
        services.AddAutoMapper(typeof(MappingProfile));
    }

    public static void AddAdminAssistantUI(this IServiceCollection services, FontAwesomeVersion fontAwesomeVersion = FontAwesomeVersion.V4o7o0)
    {
        // Add MVVM Toolkit registrations ...
        services.AddSingleton<IMessenger, StrongReferenceMessenger>();

        // Add Accounts UI ...
        services.AddSingleton<IAccountsViewModel, AccountsViewModel>();

        services.AddSingleton<IBankAccountBalanceListViewModel, BankAccountBalanceListViewModel>();
        services.AddSingleton<IBankAccountEditDialogViewModel, BankAccountEditDialogViewModel>();
        services.AddSingleton<IBankAccountRightSidebarViewModel, BankAccountRightSidebarViewModel>();
        services.AddSingleton<IBankAccountStatementImportViewModel, BankAccountStatementImportViewModel>();
        services.AddSingleton<IBankAccountTransactionListViewModel, BankAccountTransactionListViewModel>();

        services.AddTransient<IAccountsService, AccountsService>();

        services.AddSingleton<IBankListViewModel, BankListViewModel>();

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

        services.AddSingleton<ICurrencyListViewModel, CurrencyListViewModel>();

        // Add Dashboard UI ...
        services.AddSingleton<IDashboardViewModel, DashboardViewModel>();

        // Add Documents UI ...
        services.AddSingleton<IDocumentsViewModel, DocumentsViewModel>();
        services.AddTransient<IDocumentsService, DocumentsService>();

        // Add Mail UI ...
        services.AddSingleton<IMailViewModel, MailViewModel>();

        // Add Reports UI ...
        services.AddSingleton<IReportsViewModel, ReportsViewModel>();

        // Add Notes UI ...
        services.AddSingleton<INotesViewModel, NotesViewModel>();

        // Add Tasks UI ...
        services.AddSingleton<ITasksViewModel, TasksViewModel>();

        // Add Shared UI ...
        services.AddTransient<IAppService, AppService>((serviceProvider) => new AppService(fontAwesomeVersion));

        services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
    }
}
