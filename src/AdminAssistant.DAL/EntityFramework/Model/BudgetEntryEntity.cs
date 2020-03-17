namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class BudgetEntryEntity
    {
        public int BudgetEntryID { get; internal set; }
        public int BudgetID { get; internal set; }
        public int AuditID { get; internal set; }

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
