/*
Table "Contacts.ContactAddress" 
{
  "ContactAddressID" INT [pk]
  "AuditID" INT
  "AddressID" INT
  "ContactID" INT
}
*/
namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Contacts;

public class ContactAddressEntity
{
    public int ContactAddressID { get; set; }
    public int AddressID { get; set; }
    public int ContactID { get; internal set; }
}
