using AdminAssistant.Modules.AccountsModule.Builders;
using AdminAssistant.Modules.AssetRegisterModule.Builders;
using AdminAssistant.Modules.BudgetModule.Builders;
using AdminAssistant.Modules.CalendarModule.Builders;
using AdminAssistant.Modules.ContactsModule.Builders;
using AdminAssistant.Modules.CoreModule.Builders;
using AdminAssistant.Modules.DocumentsModule.Builders;
using AdminAssistant.Modules.MailModule.Builders;
using AdminAssistant.Modules.TasksModule.Builders;

namespace AdminAssistant.Domain;

public static class Factory
{
    // Accounts Module ...
    public static IBankBuilder Bank => new BankBuilder();
    public static IBankAccountBuilder BankAccount => new BankAccountBuilder();
    public static IBankAccountInfoBuilder BankAccountInfo => new BankAccountInfoBuilder();
    public static IBankAccountTypeBuilder BankAccountType => new BankAccountTypeBuilder();
    public static IBankAccountTransactionBuilder BankAccountTransaction => new BankAccountTransactionBuilder();
    public static IPayeeBuilder Payee => new PayeeBuilder();

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
