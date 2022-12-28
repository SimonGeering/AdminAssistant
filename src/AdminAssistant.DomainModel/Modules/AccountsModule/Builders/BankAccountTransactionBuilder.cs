namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

internal sealed class BankAccountTransactionBuilder : IBankAccountTransactionBuilder
{
    private BankAccountTransaction _bankAccountTransaction = new();

    public static BankAccountTransaction Default(IBankAccountTransactionBuilder builder) => builder.Build();

    public BankAccountTransaction Build() => _bankAccountTransaction;

    public IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID)
    {
        _bankAccountTransaction = _bankAccountTransaction with
        {
            BankAccountTransactionID = bankAccountTransactionID,
            BankAccountID = 10,
            Description = "Test Transaction"
        };
        return this;
    }

    public IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID)
    {
        _bankAccountTransaction = _bankAccountTransaction with { BankAccountID = bankAccountID };
        return this;
    }

    public IBankAccountTransactionBuilder WithDescription(string description)
    {
        _bankAccountTransaction = _bankAccountTransaction with { Description = description };
        return this;
    }
}
