/*
Table "Contacts.Address" 
{
  "AddressID" INT [pk]
  "AuditID" INT
}
Ref: "Contacts.Address"."AddressID" < "Contacts.ContactAddress"."AddressID"

*/
namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Contacts;

public sealed class AddressEntity
{
    public int AddressID { get; set; }
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
