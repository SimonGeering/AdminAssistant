namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class AddressEntity
    {
        public int AddressID { get; set; }
        public int AuditID { get; internal set; }

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
