namespace AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

public sealed class AddressEntity
{
    // Table "Contacts.Address"
    public int AddressID { get; set; } // PK
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    // Ref: "Contacts.Address"."AddressID" < "Contacts.ContactAddress"."AddressID"
}
