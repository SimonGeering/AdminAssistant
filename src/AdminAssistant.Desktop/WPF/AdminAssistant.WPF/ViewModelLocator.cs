using System;
using AdminAssistant.UI.Modules.AccountsModule;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountBalanceList;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountEditDialog;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountRightSidebar;
using AdminAssistant.UI.Modules.AccountsModule.BankAccountTransactionList;
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
using AdminAssistant.UI.Shared.Breadcrumb;
using AdminAssistant.UI.Shared.Footer;
using AdminAssistant.UI.Shared.Header;
using AdminAssistant.UI.Shared.Sidebar;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.WPF
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        // Add Accounts UI ...
        public IAccountsViewModel AccountsViewModel => this.serviceProvider.GetRequiredService<IAccountsViewModel>();

        public IBankAccountEditDialogViewModel BankAccountEditDialogViewModel => this.serviceProvider.GetRequiredService<IBankAccountEditDialogViewModel>();

        public IBankAccountBalanceListViewModel BankAccountBalanceListViewModel => this.serviceProvider.GetRequiredService<IBankAccountBalanceListViewModel>();
        public IBankAccountRightSidebarViewModel BankAccountRightSidebarViewModel => this.serviceProvider.GetRequiredService<IBankAccountRightSidebarViewModel>();
        public IBankAccountTransactionListViewModel BankAccountTransactionListViewModel => this.serviceProvider.GetRequiredService<IBankAccountTransactionListViewModel>();

        // Add Admin UI ...
        public IAdminViewModel AdminViewModel => this.serviceProvider.GetRequiredService<IAdminViewModel>();

        // Add Asset Register UI ...
        public IAssetRegisterViewModel AssetRegisterViewModel => this.serviceProvider.GetRequiredService<IAssetRegisterViewModel>();

        // Add Billing UI ...
        public IBillingViewModel BillingViewModel => this.serviceProvider.GetRequiredService<IBillingViewModel>();

        // Add Budget UI ...
        public IBudgetViewModel BudgetViewModel => this.serviceProvider.GetRequiredService<IBudgetViewModel>();

        // Add Calendar UI ...
        public ICalendarViewModel CalendarViewModel => this.serviceProvider.GetRequiredService<ICalendarViewModel>();

        // Add Contacts UI ...
        public IContactsViewModel ContactsViewModel => this.serviceProvider.GetRequiredService<IContactsViewModel>();

        // Add Dashboard UI ...
        public IDashboardViewModel DashboardViewModel => this.serviceProvider.GetRequiredService<IDashboardViewModel>();

        // Add Documents UI ...
        public IDocumentsViewModel DocumentsViewModel => this.serviceProvider.GetRequiredService<IDocumentsViewModel>();

        // Add Mail UI ...
        public IMailViewModel MailViewModel => this.serviceProvider.GetRequiredService<IMailViewModel>();

        // Add Reports UI ...
        public IReportsViewModel ReportsViewModel => this.serviceProvider.GetRequiredService<IReportsViewModel>();

        // Add Tasks UI ...
        public ITasksViewModel TasksViewModel => this.serviceProvider.GetRequiredService<ITasksViewModel>();

        // Add Shared UI ...
        public IBreadcrumbViewModel BreadcrumbViewModel => this.serviceProvider.GetRequiredService<IBreadcrumbViewModel>();
        public IFooterViewModel FooterViewModel => this.serviceProvider.GetRequiredService<IFooterViewModel>();
        public IHeaderViewModel HeaderViewModel => this.serviceProvider.GetRequiredService<IHeaderViewModel>();
        public ISidebarViewModel SidebarViewModel => this.serviceProvider.GetRequiredService<ISidebarViewModel>();

        public IMainWindowViewModel MainWindowViewModel => this.serviceProvider.GetRequiredService<IMainWindowViewModel>();
    }
}
