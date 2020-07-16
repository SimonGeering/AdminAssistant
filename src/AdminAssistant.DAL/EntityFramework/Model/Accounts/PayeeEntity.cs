namespace AdminAssistant.DAL.EntityFramework.Model.Accounts
{
    public class PayeeEntity
    {
        public int PayeeID { get; set; }
        public int AuditID { get; internal set; }
        public string Name { get; set; } = string.Empty;

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
