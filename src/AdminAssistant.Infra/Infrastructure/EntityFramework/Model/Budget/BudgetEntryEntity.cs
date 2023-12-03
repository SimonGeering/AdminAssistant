namespace AdminAssistant.Infrastructure.EntityFramework.Model.Budget;

public sealed class BudgetEntryEntity
{
    // Table "Budget.BudgetEntry"
    public int BudgetEntryID { get; internal set; } // PK
    public int BudgetID { get; internal set; }
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
