using AdminAssistant.Abstractions.DomainModel.Shared;

namespace AdminAssistant.DomainModel.Modules.ContactsModule;

public sealed record Contact : IDatabasePersistable
{
    public const int FirstNameMaxLength = EntityName.MaxLength;
    public const int LastNameMaxLength = EntityName.MaxLength;

    public ContactId ContactID { get; init; } = ContactId.Default;
    public int OwnerID { get; init; } = Constants.UnknownRecordID;
    public int TitleID { get; init; } = Constants.UnknownRecordID;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }


    public Id PrimaryKey => ContactID;
}
public sealed record ContactId(int Value) : Id(Value)
{
    public static ContactId Default => new(Constants.UnknownRecordID);
}
