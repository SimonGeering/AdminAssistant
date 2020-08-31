using AdminAssistant.DomainModel.Modules.AccountsModule.Builders;
using AdminAssistant.DomainModel.Modules.BudgetModule.Builders;
using AdminAssistant.DomainModel.Modules.CoreModule.Builders;
using AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;
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

        // Budget Module ...
        public static IBudgetBuilder Budget => new BudgetBuilder();

        // Core Module ...
        public static ICurrencyBuilder Currency => new CurrencyBuilder();

        // Documents Module ...
        public static IDocumentBuilder Document => new DocumentBuilder();

        // Tasks Module ...
        public static ITaskListBuilder TaskList => new TaskListBuilder();
    }
}
