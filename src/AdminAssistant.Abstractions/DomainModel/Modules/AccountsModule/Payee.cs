namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record Payee : IDatabasePersistable
{
    public const int NameMaxLength = Constants.NameMaxLength;

    public int PayeeID { get; init; }
    public string Name { get; init; } = string.Empty;

    public int PrimaryKey => PayeeID;
}
