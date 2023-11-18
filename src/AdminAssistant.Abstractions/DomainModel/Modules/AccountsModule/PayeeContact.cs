namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record PayeeContact : IDatabasePersistable
{
    public int PayeeContactID { get; init; }
    public int PayeeID { get; init; }
    public int ContactID { get; init; }
    public bool IsPrimaryContact { get; init; }

    public int PrimaryKey => PayeeContactID;
}
