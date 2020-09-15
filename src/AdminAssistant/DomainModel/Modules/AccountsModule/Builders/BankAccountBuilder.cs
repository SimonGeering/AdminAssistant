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

        public BankAccount Build() => this;

        public IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID)
        {
            BankAccountID = bankAccountID;
            AccountName = "A valid account name";
            CurrencyID = 10;
            BankAccountTypeID = 10;
            OpenedOn = DateTime.Now;
            return this;
        }

        public IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID)
        {
            BankAccountTypeID = bankAccountTypeID;
            return this;
        }
        public IBankAccountBuilder WithCurrencyID(int currencyID)
        {
            CurrencyID = currencyID;
            return this;
        }

        public IBankAccountBuilder WithAccountName(string accountName)
        {
            AccountName = accountName;
            return this;
        }
    }
}
