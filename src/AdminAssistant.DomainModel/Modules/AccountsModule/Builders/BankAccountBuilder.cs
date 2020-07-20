using System;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankAccountBuilder
    {
        BankAccount Build();
        IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID);
        IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID);
        IBankAccountBuilder WithCurrencyID(int currency);
        IBankAccountBuilder WithAccountName(string accountName);
    }
    internal class BankAccountBuilder : BankAccount, IBankAccountBuilder
    {
        public static BankAccount Default(IBankAccountBuilder builder) => builder.Build();

        public BankAccount Build()
        {
            return this;
        }

        public IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID)
        {
            this.BankAccountID = bankAccountID;
            this.AccountName = "A valid account name";
            this.CurrencyID = 10;
            this.BankAccountTypeID = 10;
            this.OpenedOn = DateTime.Now;
            return this;
        }

        public IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID)
        {
            this.BankAccountTypeID = bankAccountTypeID;
            return this;
        }
        public IBankAccountBuilder WithCurrencyID(int currencyID)
        {
            this.CurrencyID = currencyID;
            return this;
        }

        public IBankAccountBuilder WithAccountName(string accountName)
        {
            this.AccountName = accountName;
            return this;
        }
    }
}
