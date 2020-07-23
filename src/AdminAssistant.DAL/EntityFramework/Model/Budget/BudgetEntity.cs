namespace AdminAssistant.DAL.EntityFramework.Model.Budget
{
    public class BudgetEntity
    {
        public int BudgetID { get; internal set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }
        public string BudgetName { get; internal set; } = string.Empty;

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
