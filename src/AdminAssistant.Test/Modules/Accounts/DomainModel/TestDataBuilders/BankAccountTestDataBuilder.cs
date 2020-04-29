using System;

namespace AdminAssistant.DomainModel.Modules.Accounts.TestDataBuilders
{
    public interface IBankAccountTestDataBuilder
    {
        BankAccount Build();
        IBankAccountTestDataBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID);
        IBankAccountTestDataBuilder WithCurrency(Currency currency);
        IBankAccountTestDataBuilder WithAccountName(string accountName);
    }
    public class BankAccountTestDataBuilder : BankAccount, IBankAccountTestDataBuilder
    {
        public static BankAccount Default(IBankAccountTestDataBuilder builder) => builder.Build();

        public BankAccount Build()
        {
            return this;
        }

        public IBankAccountTestDataBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID)
        {
            this.BankAccountID = bankAccountID;
            this.AccountName = "A valid account name";
            this.Currency = TestData.CurrencyBuilder.WithTestData().Build();
            this.OpenedOn = DateTime.Now;
            return this;
        }

        public IBankAccountTestDataBuilder WithCurrency(Currency currency)
        {
            this.Currency = currency;
            return this;
        }

        public IBankAccountTestDataBuilder WithAccountName(string accountName)
        {
            this.AccountName = accountName;
            return this;
        }
    }
}
