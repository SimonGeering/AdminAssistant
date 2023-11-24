using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AdminModule;
using AdminAssistant.UI.Modules.AssetRegisterModule;
using AdminAssistant.UI.Modules.BillingModule;
using AdminAssistant.UI.Modules.BudgetModule;
using AdminAssistant.UI.Modules.CalendarModule;
using AdminAssistant.UI.Modules.ContactsModule;
using AdminAssistant.UI.Modules.DashboardModule;
using AdminAssistant.UI.Modules.DocumentsModule;
using AdminAssistant.UI.Modules.MailModule;
using AdminAssistant.UI.Modules.ReportsModule;
using AdminAssistant.UI.Modules.TasksModule;
using AdminAssistant.UI.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.WPF;

public sealed class ViewModelLocator(IServiceProvider serviceProvider)
{
    // Add Accounts UI ...
    public IAccountsViewModel AccountsViewModel => serviceProvider.GetRequiredService<IAccountsViewModel>();

    public IBankAccountEditDialogViewModel BankAccountEditDialogViewModel => serviceProvider.GetRequiredService<IBankAccountEditDialogViewModel>();

    public IBankAccountBalanceListViewModel BankAccountBalanceListViewModel => serviceProvider.GetRequiredService<IBankAccountBalanceListViewModel>();
    public IBankAccountRightSidebarViewModel BankAccountRightSidebarViewModel => serviceProvider.GetRequiredService<IBankAccountRightSidebarViewModel>();
    public IBankAccountTransactionListViewModel BankAccountTransactionListViewModel => serviceProvider.GetRequiredService<IBankAccountTransactionListViewModel>();

    // Add Admin UI ...
    public IAdminViewModel AdminViewModel => serviceProvider.GetRequiredService<IAdminViewModel>();

    // Add Asset Register UI ...
    public IAssetRegisterViewModel AssetRegisterViewModel => serviceProvider.GetRequiredService<IAssetRegisterViewModel>();

    // Add Billing UI ...
    public IBillingViewModel BillingViewModel => serviceProvider.GetRequiredService<IBillingViewModel>();

    // Add Budget UI ...
    public IBudgetViewModel BudgetViewModel => serviceProvider.GetRequiredService<IBudgetViewModel>();

    // Add Calendar UI ...
    public ICalendarViewModel CalendarViewModel => serviceProvider.GetRequiredService<ICalendarViewModel>();

    // Add Contacts UI ...
    public IContactsViewModel ContactsViewModel => serviceProvider.GetRequiredService<IContactsViewModel>();

    // Add Dashboard UI ...
    public IDashboardViewModel DashboardViewModel => serviceProvider.GetRequiredService<IDashboardViewModel>();

    // Add Documents UI ...
    public IDocumentsViewModel DocumentsViewModel => serviceProvider.GetRequiredService<IDocumentsViewModel>();

    // Add Mail UI ...
    public IMailViewModel MailViewModel => serviceProvider.GetRequiredService<IMailViewModel>();

    // Add Reports UI ...
    public IReportsViewModel ReportsViewModel => serviceProvider.GetRequiredService<IReportsViewModel>();

    // Add Tasks UI ...
    public ITasksViewModel TasksViewModel => serviceProvider.GetRequiredService<ITasksViewModel>();

    // Add Shared UI ...
    public IMainWindowViewModel MainWindowViewModel => serviceProvider.GetRequiredService<IMainWindowViewModel>();
}
