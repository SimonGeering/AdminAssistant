namespace AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

public sealed class ContactAddressEntity
{
    // Table "Contacts.ContactAddress"
    public int ContactAddressID { get; set; } // PK
    public int AddressID { get; set; }
    public int ContactID { get; internal set; }
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
