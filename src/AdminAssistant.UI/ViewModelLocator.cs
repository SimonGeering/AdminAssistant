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

public sealed class ViewModelLocator
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelLocator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    // Add Accounts UI ...
    public IAccountsViewModel AccountsViewModel => _serviceProvider.GetRequiredService<IAccountsViewModel>();

    public IBankAccountEditDialogViewModel BankAccountEditDialogViewModel => _serviceProvider.GetRequiredService<IBankAccountEditDialogViewModel>();

    public IBankAccountBalanceListViewModel BankAccountBalanceListViewModel => _serviceProvider.GetRequiredService<IBankAccountBalanceListViewModel>();
    public IBankAccountRightSidebarViewModel BankAccountRightSidebarViewModel => _serviceProvider.GetRequiredService<IBankAccountRightSidebarViewModel>();
    public IBankAccountTransactionListViewModel BankAccountTransactionListViewModel => _serviceProvider.GetRequiredService<IBankAccountTransactionListViewModel>();

    // Add Admin UI ...
    public IAdminViewModel AdminViewModel => _serviceProvider.GetRequiredService<IAdminViewModel>();

    // Add Asset Register UI ...
    public IAssetRegisterViewModel AssetRegisterViewModel => _serviceProvider.GetRequiredService<IAssetRegisterViewModel>();

    // Add Billing UI ...
    public IBillingViewModel BillingViewModel => _serviceProvider.GetRequiredService<IBillingViewModel>();

    // Add Budget UI ...
    public IBudgetViewModel BudgetViewModel => _serviceProvider.GetRequiredService<IBudgetViewModel>();

    // Add Calendar UI ...
    public ICalendarViewModel CalendarViewModel => _serviceProvider.GetRequiredService<ICalendarViewModel>();

    // Add Contacts UI ...
    public IContactsViewModel ContactsViewModel => _serviceProvider.GetRequiredService<IContactsViewModel>();

    // Add Dashboard UI ...
    public IDashboardViewModel DashboardViewModel => _serviceProvider.GetRequiredService<IDashboardViewModel>();

    // Add Documents UI ...
    public IDocumentsViewModel DocumentsViewModel => _serviceProvider.GetRequiredService<IDocumentsViewModel>();

    // Add Mail UI ...
    public IMailViewModel MailViewModel => _serviceProvider.GetRequiredService<IMailViewModel>();

    // Add Reports UI ...
    public IReportsViewModel ReportsViewModel => _serviceProvider.GetRequiredService<IReportsViewModel>();

    // Add Tasks UI ...
    public ITasksViewModel TasksViewModel => _serviceProvider.GetRequiredService<ITasksViewModel>();

    // Add Shared UI ...
    public IMainWindowViewModel MainWindowViewModel => _serviceProvider.GetRequiredService<IMainWindowViewModel>();
}
