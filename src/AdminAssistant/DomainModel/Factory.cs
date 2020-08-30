using AdminAssistant.DomainModel.Modules.AccountsModule.Builders;
using AdminAssistant.DomainModel.Modules.DocumentsModule.Builders;

namespace AdminAssistant.DomainModel
{
    public static class Factory
    {
        public static IBankBuilder Bank => new BankBuilder();
        public static IBankAccountBuilder BankAccount => new BankAccountBuilder();
        public static IBankAccountInfoBuilder BankAccountInfo => new BankAccountInfoBuilder();
        public static IBankAccountTypeBuilder BankAccountType => new BankAccountTypeBuilder();
        public static ICurrencyBuilder Currency => new CurrencyBuilder();
        public static IDocumentBuilder Document => new DocumentBuilder();
        public static IBankAccountTransactionBuilder BankAccountTransaction => new BankAccountTransactionBuilder();
    }
}
