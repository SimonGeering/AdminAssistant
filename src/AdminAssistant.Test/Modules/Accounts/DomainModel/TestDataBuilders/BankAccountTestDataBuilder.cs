using System;

namespace AdminAssistant.DomainModel.Modules.Accounts.TestDataBuilders
{
    public interface IBankAccountTestDataBuilder
    {
        BankAccount Build();
        IBankAccountTestDataBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID);
        IBankAccountTestDataBuilder WithBankAccountTypeID(int bankAccountTypeID);
        IBankAccountTestDataBuilder WithCurrencyID(int currency);
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
            this.CurrencyID = 10;
            this.BankAccountTypeID = 10;
            this.OpenedOn = DateTime.Now;
            return this;
        }

        public IBankAccountTestDataBuilder WithBankAccountTypeID(int bankAccountTypeID)
        {
            this.BankAccountTypeID = bankAccountTypeID;
            return this;
        }
        public IBankAccountTestDataBuilder WithCurrencyID(int currencyID)
        {
            this.CurrencyID = currencyID;
            return this;
        }

        public IBankAccountTestDataBuilder WithAccountName(string accountName)
        {
            this.AccountName = accountName;
            return this;
        }
    }
}
