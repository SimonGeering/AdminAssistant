namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankAccountTypeBuilder
    {
        BankAccountType Build();
        IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID);
        IBankAccountTypeBuilder WithDescription(string description);
    }
    public class BankAccountTypeBuilder : BankAccountType, IBankAccountTypeBuilder
    {
        public static BankAccountType Default(IBankAccountTypeBuilder builder) => builder.Build();

        public BankAccountType Build()
        {
            return this;
        }

        public IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID)
        {
            this.BankAccountTypeID = bankAccountTypeID;
            this.Description = "A valid BankAccountType description";
            return this;
        }

        public IBankAccountTypeBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }
    }
}
