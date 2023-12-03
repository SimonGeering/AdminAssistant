namespace AdminAssistant.Modules.AccountsModule.Builders;

public interface IBankAccountTransactionBuilder
{
    BankAccountTransaction Build();
    IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID);
    IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID);
    IBankAccountTransactionBuilder WithDescription(string description);
}
internal sealed class BankAccountTransactionBuilder : IBankAccountTransactionBuilder
{
    private BankAccountTransaction _bankAccountTransaction = new();

    public static BankAccountTransaction Default(IBankAccountTransactionBuilder builder) => builder.Build();

    public BankAccountTransaction Build() => _bankAccountTransaction;

    public IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID)
    {
        _bankAccountTransaction = _bankAccountTransaction with
        {
            BankAccountTransactionID = new(bankAccountTransactionID),
            BankAccountID = new(10),
            Description = "Test Transaction"
        };
        return this;
    }

    public IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID)
    {
        _bankAccountTransaction = _bankAccountTransaction with { BankAccountID = new(bankAccountID) };
        return this;
    }

    public IBankAccountTransactionBuilder WithDescription(string description)
    {
        _bankAccountTransaction = _bankAccountTransaction with { Description = description };
        return this;
    }
}
