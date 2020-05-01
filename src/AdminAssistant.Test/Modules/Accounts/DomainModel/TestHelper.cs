using AdminAssistant.DomainModel.Modules.Accounts.TestDataBuilders;

namespace AdminAssistant.DomainModel.Modules.Accounts
{
    public static class TestData
    {
        public static IBankAccountTestDataBuilder BankAccountBuilder => new BankAccountTestDataBuilder();
        public static IBankAccountTypeTestDataBuilder BankAccountTypeBuilder => new BankAccountTypeTestDataBuilder();
        public static ICurrencyTestDataBuilder CurrencyBuilder => new CurrencyTestDataBuilder();
    }
}
