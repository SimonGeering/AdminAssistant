/*

Table "Budget.BudgetEntry" 
{
  "BudgetEntryID" INT [pk]
  "BudgetID" INT
  "AuditID" INT
}

*/

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Budget;

public sealed class BudgetEntryEntity
{
    public int BudgetEntryID { get; internal set; }
    public int BudgetID { get; internal set; }
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
