using AdminAssistant.Modules.ContactsModule.UI;
using AdminAssistant.Modules.ReportsModule.UI;
using AdminAssistant.Modules.TasksModule.UI;
using AdminAssistant.Shared.UI;
using AdminAssistant.Modules.AccountsModule.UI;
using AdminAssistant.Modules.AdminModule.AdminUI;
using AdminAssistant.Modules.AssetRegisterModule.UI;
using AdminAssistant.Modules.BillingModule.UI;
using AdminAssistant.Modules.BudgetModule.UI;
using AdminAssistant.Modules.CalendarModule.UI;
using AdminAssistant.Modules.DashboardModule.UI;
using AdminAssistant.Modules.DocumentsModule.UI;
using AdminAssistant.Modules.MailModule.UI;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Primitives.UI;

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
