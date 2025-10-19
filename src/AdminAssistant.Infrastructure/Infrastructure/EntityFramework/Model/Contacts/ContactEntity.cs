namespace AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

public sealed class ContactEntity
{
    // Table "Contacts.Contact"
    public int ContactID { get; set; } // PK
    public int AuditID { get; internal set; }
    public int OwnerID { get; set; }
    public int TitleID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;

    // Ref: "Contacts.Contact"."ContactID" < "Contacts.ContactAddress"."ContactID"
    // Ref: "Contacts.Contact"."ContactID" < "Accounts.PayeeContact"."ContactID"
}
