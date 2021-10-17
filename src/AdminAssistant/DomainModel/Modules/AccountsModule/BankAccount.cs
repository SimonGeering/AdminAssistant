namespace AdminAssistant.DomainModel.Modules.AccountsModule;

/// <summary>
/// A financial account held at a given <see cref="Bank"/>.
/// </summary>
/// <seealso cref="IDatabasePersistable"/>
public record BankAccount : IDatabasePersistable
{
    public const int AccountNameMaxLength = Constants.NameMaxLength;

    /// <summary>
    /// Unique identifier for the <see cref="BankAccount"/>.
    /// </summary>
    public int BankAccountID { get; init; }

    /// <summary>
    /// The unique identifier of the <see cref="BankAccountType"/> for this <see cref="BankAccount"/>./>
    /// </summary>
    public int BankAccountTypeID { get; init; } = Constants.UnknownRecordID;

    /// <summary>
    /// The unique identifier of the <see cref="CoreModule.Currency"/> for this <see cref="BankAccount"/>./>
    /// </summary>
    public int CurrencyID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }
    public int OpeningBalance { get; init; }
    public int CurrentBalance { get; init; }
    public DateTime OpenedOn { get; init; }

    /// <inheritdoc/>
    public int PrimaryKey => BankAccountID;
}
