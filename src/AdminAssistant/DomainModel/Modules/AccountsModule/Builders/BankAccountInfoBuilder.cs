using System;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankAccountInfoBuilder
    {
        BankAccountInfo Build();
        IBankAccountInfoBuilder WithTestData(int bankAccountInfoID = Constants.UnknownRecordID);
    }
    internal class BankAccountInfoBuilder : BankAccountInfo, IBankAccountInfoBuilder
    {
        public static BankAccountInfo Default(IBankAccountInfoBuilder builder) => builder.Build();

        public BankAccountInfo Build()
        {
            return this;
        }

        public IBankAccountInfoBuilder WithTestData(int bankAccountID = 0)
        {
            this.BankAccountID = bankAccountID;
            this.AccountName = "A valid account name";
            this.CurrentBalance = 0;
            this.Symbol = "GBP";
            this.DecimalFormat = "2.2-2";
            this.IsBudgeted = false;

            return this;
        }
    }
}
