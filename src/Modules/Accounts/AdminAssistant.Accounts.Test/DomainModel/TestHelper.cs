using AdminAssistant.Accounts.DomainModel.TestDataBuilders;

namespace AdminAssistant.Accounts.DomainModel
{
    public static class TestData
    {
        public static IBankAccountTestDataBuilder BankAccountBuilder => new BankAccountTestDataBuilder();
        public static ICurrencyTestDataBuilder CurrencyBuilder => new CurrencyTestDataBuilder();
    }
}
