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

        public BankAccountInfo Build() => this;

        public IBankAccountInfoBuilder WithTestData(int bankAccountID = 0)
        {
            BankAccountID = bankAccountID;
            AccountName = "A valid account name";
            CurrentBalance = 0;
            Symbol = "GBP";
            DecimalFormat = "2.2-2";
            IsBudgeted = false;

            return this;
        }
    }
}
