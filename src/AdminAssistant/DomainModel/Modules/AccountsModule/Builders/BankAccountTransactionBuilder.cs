using System;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankAccountTransactionBuilder
    {
        BankAccountTransaction Build();
        IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID);
        IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID);
        IBankAccountTransactionBuilder WithDescription(string empty);
    }
    internal class BankAccountTransactionBuilder : BankAccountTransaction, IBankAccountTransactionBuilder
    {
        public static BankAccountTransaction Default(IBankAccountTransactionBuilder builder) => builder.Build();

        public BankAccountTransaction Build()
        {
            return this;
        }

        public IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID)
        {
            this.BankAccountTransactionID = bankAccountTransactionID;
            this.BankAccountID = 10;
            this.Description = "Test Transaction";
            return this;
        }

        public IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
            return this;
        }

        public IBankAccountTransactionBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }
    }
}
