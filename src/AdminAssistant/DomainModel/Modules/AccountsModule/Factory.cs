using AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public static class Factory
    {
        public static IBankAccountBuilder BankAccount => new BankAccountBuilder();
        public static IBankAccountTypeBuilder BankAccountType => new BankAccountTypeBuilder();
        public static ICurrencyBuilder Currency => new CurrencyBuilder();
        public static IBankAccountTransactionBuilder BankAccountTransaction => new BankAccountTransactionBuilder();
    }
}
