namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankAccountTypeBuilder
    {
        BankAccountType Build();
        IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID);
        IBankAccountTypeBuilder WithDescription(string description);
    }
    internal class BankAccountTypeBuilder : BankAccountType, IBankAccountTypeBuilder
    {
        public static BankAccountType Default(IBankAccountTypeBuilder builder) => builder.Build();

        public BankAccountType Build() => this;

        public IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID)
        {
            BankAccountTypeID = bankAccountTypeID;
            Description = "A valid BankAccountType description";
            return this;
        }

        public IBankAccountTypeBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}
