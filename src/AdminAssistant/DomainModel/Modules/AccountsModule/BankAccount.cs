using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public record BankAccount : IDatabasePersistable
{
    public const int AccountNameMaxLength = Constants.NameMaxLength;

    public int BankAccountID { get; init; }
    public int BankAccountTypeID { get; init; } = Constants.UnknownRecordID;
    public int CurrencyID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }
    public int OpeningBalance { get; init; }
    public int CurrentBalance { get; init; }
    public DateTime OpenedOn { get; init; }

    public int PrimaryKey => BankAccountID;
}
