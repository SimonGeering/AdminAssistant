namespace AdminAssistant.DomainModel.Modules.ContactsModule;

public sealed record Contact : IDatabasePersistable
{
    public const int FirstNameMaxLength = Constants.NameMaxLength;
    public const int LastNameMaxLength = Constants.NameMaxLength;

    public int ContactID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public int PrimaryKey => ContactID;
}
