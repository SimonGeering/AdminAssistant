namespace AdminAssistant.DAL.EntityFramework.Model.Budget
{
    public class BudgetEntryEntity
    {
        public int BudgetEntryID { get; internal set; }
        public int BudgetID { get; internal set; }
        public int AuditID { get; internal set; }

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
