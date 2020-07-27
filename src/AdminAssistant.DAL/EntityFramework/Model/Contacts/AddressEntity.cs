namespace AdminAssistant.DAL.EntityFramework.Model.Contacts
{
    public class AddressEntity
    {
        public int AddressID { get; set; }
        public int AuditID { get; internal set; }

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
