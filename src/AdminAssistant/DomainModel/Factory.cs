using AdminAssistant.DomainModel.Modules.AccountsModule.Builders;
using AdminAssistant.DomainModel.Modules.AssetRegisterModule.Builders;
using AdminAssistant.DomainModel.Modules.BudgetModule.Builders;
using AdminAssistant.DomainModel.Modules.CalendarModule.Builders;
using AdminAssistant.DomainModel.Modules.ContactsModule.Builders;
using AdminAssistant.DomainModel.Modules.CoreModule.Builders;
using AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;
using AdminAssistant.DomainModel.Modules.MailModule.Builders;
using AdminAssistant.DomainModel.Modules.TasksModule.Builders;

namespace AdminAssistant.DomainModel
{
    public static class Factory
    {
        // Accounts Module ...
        public static IBankBuilder Bank => new BankBuilder();
        public static IBankAccountBuilder BankAccount => new BankAccountBuilder();
        public static IBankAccountInfoBuilder BankAccountInfo => new BankAccountInfoBuilder();
        public static IBankAccountTypeBuilder BankAccountType => new BankAccountTypeBuilder();
        public static IBankAccountTransactionBuilder BankAccountTransaction => new BankAccountTransactionBuilder();

        // Asset Register Module ...
        public static IAssetBuilder Asset => new AssetBuilder();

        // Budget Module ...
        public static IBudgetBuilder Budget => new BudgetBuilder();

        // Calendar Module ...
        public static IReminderBuilder Reminder => new ReminderBuilder();

        // Contact Module ...
        public static IContactBuilder Contact => new ContactBuilder();        

        // Core Module ...
        public static ICurrencyBuilder Currency => new CurrencyBuilder();

        // Documents Module ...
        public static IDocumentBuilder Document => new DocumentBuilder();

        // Mail Module ...
        public static IMailMessageBuilder MailMessage => new MailMessageBuilder();

        // Tasks Module ...
        public static ITaskListBuilder TaskList => new TaskListBuilder();
    }
}
