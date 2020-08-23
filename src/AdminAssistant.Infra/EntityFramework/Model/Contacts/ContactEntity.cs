/*
Table "Contacts.Contact" 
{
  "ContactID" INT [pk]
  "AuditID" INT
  "OwnerID" INT
  "TitleID" INT
  "FirstName" NVARCHAR(50)
  "LastName" NVARCHAR(50)
  "DateOfBirth" DATETIME2
}
Ref: "Contacts.Contact"."ContactID" < "Contacts.ContactAddress"."ContactID"
Ref: "Contacts.Contact"."ContactID" < "Accounts.PayeeContact"."ContactID"

*/
using System;

namespace AdminAssistant.DAL.EntityFramework.Model.Contacts
{
    public class ContactEntity
    {
        public int ContactID { get; set; }
        public int OwnerID { get; set; }
        public int TitleID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int AuditID { get; internal set; }

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
