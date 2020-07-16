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

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
