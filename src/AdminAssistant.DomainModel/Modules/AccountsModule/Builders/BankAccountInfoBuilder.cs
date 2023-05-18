namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

internal sealed class BankAccountInfoBuilder : IBankAccountInfoBuilder
{
    private BankAccountInfo _bankAccountInfo = new();

    public static BankAccountInfo Default(IBankAccountInfoBuilder builder) => builder.Build();

    public BankAccountInfo Build() => _bankAccountInfo;

    public IBankAccountInfoBuilder WithTestData(int bankAccountInfoID = 0)
    {
        _bankAccountInfo = _bankAccountInfo with
        {
            BankAccountID = bankAccountInfoID,
            AccountName = "A valid account name",
            CurrentBalance = 0,
            Symbol = "GBP",
            DecimalFormat = "2.2-2",
            IsBudgeted = false
        };
        return this;
    }
}
