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

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantUI(this IServiceCollection services, FontAwesomeVersion fontAwesomeVersion = FontAwesomeVersion.V4o7o0)
    {
        // Add MVVM Toolkit registrations ...
        services.AddSingleton<IMessenger, StrongReferenceMessenger>();

        // Add Accounts UI ...
        services.AddSingleton<IAccountsViewModel, AccountsViewModel>();
        services.AddSingleton<AccountsDesignerViewModel>();

        services.AddSingleton<IBankAccountBalanceListViewModel, BankAccountBalanceListViewModel>();
        services.AddSingleton<BankAccountBalanceListDesignerViewModel>();
        services.AddSingleton<IBankAccountEditDialogViewModel, BankAccountEditDialogViewModel>();
        services.AddSingleton<BankAccountEditDialogDesignerViewModel>();
        services.AddSingleton<IBankAccountRightSidebarViewModel, BankAccountRightSidebarViewModel>();
        services.AddSingleton<BankAccountRightSidebarDesignerViewModel>();
        services.AddSingleton<IBankAccountStatementImportViewModel, BankAccountStatementImportViewModel>();
        services.AddSingleton<BankAccountStatementImportDesignerViewModel>();
        services.AddSingleton<IBankAccountTransactionListViewModel, BankAccountTransactionListViewModel>();
        services.AddSingleton<BankAccountTransactionListDesignerViewModel>();

        services.AddTransient<IAccountsService, AccountsService>();

        services.AddSingleton<IBankListViewModel, BankListViewModel>();
        services.AddSingleton<BankListDesignerViewModel>();

        // Add Admin UI ...
        services.AddSingleton<IAdminViewModel, AdminViewModel>();
        services.AddSingleton<AdminDesignerViewModel>();

        // Add Asset Register UI ...
        services.AddSingleton<IAssetRegisterViewModel, AssetRegisterViewModel>();
        services.AddSingleton<AssetRegisterDesignerViewModel>();

        // Add Billing UI ...
        services.AddSingleton<IBillingViewModel, BillingViewModel>();
        services.AddSingleton<BillingDesignerViewModel>();

        // Add Budget UI ...
        services.AddSingleton<IBudgetViewModel, BudgetViewModel>();
        services.AddSingleton<BudgetDesignerViewModel>();

        // Add Calendar UI ...
        services.AddSingleton<ICalendarViewModel, CalendarViewModel>();
        services.AddSingleton<CalendarDesignerViewModel>();

        // Add Contacts UI ...
        services.AddSingleton<IContactsViewModel, ContactsViewModel>();
        services.AddSingleton<ContactsDesignerViewModel>();

        // Add Core UI ...
        services.AddSingleton<ICurrencyListViewModel, CurrencyListViewModel>();
        services.AddSingleton<CurrencyListDesignerViewModel>();

        services.AddTransient<ICoreService, CoreService>();

        // Add Dashboard UI ...
        services.AddSingleton<IDashboardViewModel, DashboardViewModel>();
        services.AddSingleton<DashboardDesignerViewModel>();

        // Add Documents UI ...
        services.AddSingleton<IDocumentsViewModel, DocumentsViewModel>();
        services.AddSingleton<DocumentsDesignerViewModel>();

        services.AddTransient<IDocumentsService, DocumentsService>();

        // Add Mail UI ...
        services.AddSingleton<IMailViewModel, MailViewModel>();
        services.AddSingleton<MailDesignerViewModel>();

        // Add Reports UI ...
        services.AddSingleton<IReportsViewModel, ReportsViewModel>();
        services.AddSingleton<ReportsDesignerViewModel>();

        // Add Notes UI ...
        services.AddSingleton<INotesViewModel, NotesViewModel>();
        services.AddSingleton<NotesDesignerViewModel>();

        // Add Tasks UI ...
        services.AddSingleton<ITasksViewModel, TasksViewModel>();
        services.AddSingleton<TasksDesignerViewModel>();

        // Add Shared UI ...
        services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();

        services.AddTransient<IAppService, AppService>((serviceProvider) => new AppService(fontAwesomeVersion));
    }
}
