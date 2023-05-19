namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Budget;

public sealed class BudgetEntity
{
    // Table "Budget.Budget"
    public int BudgetID { get; internal set; } // PK
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }
    public string BudgetName { get; set; } = string.Empty;

    public Core.AuditEntity Audit { get; internal set; } = null!;
    // Ref: "Budget.Budget"."BudgetID" < "Budget.BudgetEntry"."BudgetID"
}
