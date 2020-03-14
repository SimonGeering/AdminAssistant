namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class BudgetEntity
    {
        public int BudgetID { get; internal set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }
        public string BudgetName { get; internal set; } = string.Empty;

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
