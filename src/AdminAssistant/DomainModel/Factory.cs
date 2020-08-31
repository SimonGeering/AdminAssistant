using AdminAssistant.DomainModel.Modules.AccountsModule.Builders;
using AdminAssistant.DomainModel.Modules.BudgetModule.Builders;
using AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;
using AdminAssistant.DomainModel.Modules.TasksModule.Builders;

namespace AdminAssistant.DomainModel
{
    public static class Factory
    {
        // AccountsModule ...
        public static IBankBuilder Bank => new BankBuilder();
        public static IBankAccountBuilder BankAccount => new BankAccountBuilder();
        public static IBankAccountInfoBuilder BankAccountInfo => new BankAccountInfoBuilder();
        public static IBankAccountTypeBuilder BankAccountType => new BankAccountTypeBuilder();
        public static IBankAccountTransactionBuilder BankAccountTransaction => new BankAccountTransactionBuilder();
        public static ICurrencyBuilder Currency => new CurrencyBuilder();

        // BudgetModule ...
        public static IBudgetBuilder Budget => new BudgetBuilder();

        // DocumentsModule ...
        public static IDocumentBuilder Document => new DocumentBuilder();

        // TasksModule ...
        public static ITaskListBuilder TaskList => new TaskListBuilder();
    }
}
