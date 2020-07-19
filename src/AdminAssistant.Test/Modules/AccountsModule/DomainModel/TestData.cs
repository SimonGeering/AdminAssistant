using AdminAssistant.DomainModel.Modules.AccountsModule.TestDataBuilders;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public static class TestData
    {
        public static IBankAccountTestDataBuilder BankAccountBuilder => new BankAccountTestDataBuilder();
        public static IBankAccountTypeTestDataBuilder BankAccountTypeBuilder => new BankAccountTypeTestDataBuilder();
        public static ICurrencyTestDataBuilder CurrencyBuilder => new CurrencyTestDataBuilder();
    }
}
