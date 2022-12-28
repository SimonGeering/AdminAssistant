namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Grocery;

public sealed class MealPlanEntity
{
    public int MealPlanID { get; set; }
    public int AuditID { get; set; }
    public int OwnerID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
