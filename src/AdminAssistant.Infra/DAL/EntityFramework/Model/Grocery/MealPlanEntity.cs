namespace AdminAssistant.DAL.EntityFramework.Model.Grocery
{
    public class MealPlanEntity
    {
        public int MealPlanID { get; set; }
        public int AuditID { get; set; }
        public int OwnerID { get; internal set; }

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
