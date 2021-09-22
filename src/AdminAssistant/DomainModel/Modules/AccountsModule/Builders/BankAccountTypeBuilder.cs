namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountTypeBuilder
{
    BankAccountType Build();
    IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID);
    IBankAccountTypeBuilder WithDescription(string description);
}
internal class BankAccountTypeBuilder : IBankAccountTypeBuilder
{
    private BankAccountType _bankAccountType = new();
    public static BankAccountType Default(IBankAccountTypeBuilder builder) => builder.Build();

    public BankAccountType Build() => _bankAccountType;

    public IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID)
    {
        _bankAccountType = _bankAccountType with
        {
            BankAccountTypeID = bankAccountTypeID,
            Description = "A valid BankAccountType description"
        };
        return this;
    }

    public IBankAccountTypeBuilder WithDescription(string description)
    {
        _bankAccountType = _bankAccountType with { Description = description };
        return this;
    }
}
