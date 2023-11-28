namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

internal sealed class BankAccountBuilder : IBankAccountBuilder
{
    private BankAccount _bankAccount = new();

    public static BankAccount Default(IBankAccountBuilder builder) => builder.Build();

    public BankAccount Build() => _bankAccount;

    public IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID)
    {
        _bankAccount = _bankAccount with
        {
            BankAccountID = new (bankAccountID),
            AccountName = "A valid account name",
            CurrencyID = new (10),
            BankAccountTypeID = new(10),
            OpenedOn = DateTime.Now
        };
        return this;
    }

    public IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID)
    {
        _bankAccount = _bankAccount with { BankAccountTypeID = new(bankAccountTypeID) };
        return this;
    }
    public IBankAccountBuilder WithCurrencyID(int currencyID)
    {
        _bankAccount = _bankAccount with { CurrencyID = new(currencyID) };
        return this;
    }

    public IBankAccountBuilder WithAccountName(string accountName)
    {
        _bankAccount = _bankAccount with { AccountName = accountName };
        return this;
    }
}

