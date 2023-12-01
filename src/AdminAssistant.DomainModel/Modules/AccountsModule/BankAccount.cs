using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Shared;

namespace AdminAssistant.Modules.AccountsModule;

/// <summary>
/// A financial account held at a given <see cref="Bank"/>.
/// </summary>
/// <seealso cref="IPersistable"/>
public sealed record BankAccount : IPersistable
{
    public const int AccountNameMaxLength = EntityName.MaxLength;

    /// <summary>
    /// Unique identifier for the <see cref="BankAccount"/>.
    /// </summary>
    public BankAccountId BankAccountID { get; init; } = BankAccountId.Default;

    /// <summary>
    /// The unique identifier of the <see cref="BankAccountType"/> for this <see cref="BankAccount"/>./>
    /// </summary>
    public BankAccountTypeId BankAccountTypeID { get; init; } = BankAccountTypeId.Default;

    /// <summary>
    /// The unique identifier of the <see cref="CoreModule.Domain.Currency"/> for this <see cref="BankAccount"/>./>
    /// </summary>
    public CurrencyId CurrencyID { get; init; } = CurrencyId.Default;

    /// <summary>
    /// The unique identifier of the owner of this <see cref="BankAccount"/>./>
    /// </summary>
    public int OwnerID { get; init; }

    public string AccountName { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }
    public int OpeningBalance { get; init; }
    public int CurrentBalance { get; init; }
    public DateTime OpenedOn { get; init; }

    /// <inheritdoc/>
    public Id PrimaryKey => BankAccountID;
}
public sealed record BankAccountId(int Value) : Id(Value)
{
    public static BankAccountId Default => new(Constants.UnknownRecordID);
}
