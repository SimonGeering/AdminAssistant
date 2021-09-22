namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountTransactionBuilder
{
    BankAccountTransaction Build();
    IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID);
    IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID);
    IBankAccountTransactionBuilder WithDescription(string empty);
}
internal class BankAccountTransactionBuilder : IBankAccountTransactionBuilder
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
