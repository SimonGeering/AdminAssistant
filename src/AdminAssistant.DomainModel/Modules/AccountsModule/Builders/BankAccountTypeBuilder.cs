namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

internal sealed class BankAccountTypeBuilder : IBankAccountTypeBuilder
{
    private BankAccountType _bankAccountType = new();
    public static BankAccountType Default(IBankAccountTypeBuilder builder) => builder.Build();

    public BankAccountType Build() => _bankAccountType;

    public IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID)
    {
        _bankAccountType = _bankAccountType with
        {
            BankAccountTypeID = new(bankAccountTypeID),
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
