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

        public BankAccountTransaction Build() => this;

        public IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID)
        {
            BankAccountTransactionID = bankAccountTransactionID;
            BankAccountID = 10;
            Description = "Test Transaction";
            return this;
        }

        public IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID)
        {
            BankAccountID = bankAccountID;
            return this;
        }

        public IBankAccountTransactionBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}
