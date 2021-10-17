namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public record Bank : IDatabasePersistable
{
    public const int BankNameMaxLength = Constants.NameMaxLength;

    public int BankID { get; init; } = Constants.UnknownRecordID;
    public string BankName { get; init; } = string.Empty;

    ///<inheritdoc/>
    public int PrimaryKey => BankID;
}
