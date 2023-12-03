namespace AdminAssistant.Modules.AccountsModule.Builders;

/// <summary>
/// Creates a <see cref="BankAccount"/> instance.
/// </summary>
public interface IBankAccountBuilder
{
    /// <summary>
    /// Creates the <see cref="BankAccount"/> instance using the data passed in other method calls.
    /// </summary>
    /// <returns>
    /// A populated <see cref="BankAccount"/> instance.
    /// </returns>
    BankAccount Build();

    /// <summary>
    /// Populates a set of safe sample test data.
    /// </summary>
    /// <param name="bankAccountID">Optionally specify the <see cref="BankAccount.BankAccountID"/> value to use.</param>
    /// <returns>The <see cref="IBankAccountBuilder"/> instance to allow fluent chaining.</returns>
    IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID);

    /// <summary>
    /// Sets the <see cref="BankAccount.BankAccountTypeID"/> value. 
    /// </summary>
    /// <param name="bankAccountTypeID">The value to set the property to.</param>
    /// <returns>The <see cref="IBankAccountBuilder"/> instance to allow fluent chaining.</returns>
    IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID);

    /// <summary>
    /// Sets the <see cref="BankAccount.CurrencyID"/> value. 
    /// </summary>
    /// <param name="currencyID">The value to set the property to.</param>
    /// <returns>The <see cref="IBankAccountBuilder"/> instance to allow fluent chaining.</returns>
    IBankAccountBuilder WithCurrencyID(int currencyID);

    /// <summary>
    /// Sets the <see cref="BankAccount.AccountName"/> value. 
    /// </summary>
    /// <param name="accountName">The value to set the property to.</param>
    /// <returns>The <see cref="IBankAccountBuilder"/> instance to allow fluent chaining.</returns>
    IBankAccountBuilder WithAccountName(string accountName);
}
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

