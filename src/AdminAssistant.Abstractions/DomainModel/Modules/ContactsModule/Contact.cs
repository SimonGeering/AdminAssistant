namespace AdminAssistant.DomainModel.Modules.ContactsModule;

public sealed record Contact : IDatabasePersistable
{
    public const int FirstNameMaxLength = Constants.NameMaxLength;
    public const int LastNameMaxLength = Constants.NameMaxLength;

    public ContactId ContactID { get; init; } = ContactId.Default;
    public int OwnerID { get; init; } = Constants.UnknownRecordID;
    public int TitleID { get; init; } = Constants.UnknownRecordID;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }


    public int PrimaryKey => ContactID.Value;
}
