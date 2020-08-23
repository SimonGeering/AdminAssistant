namespace AdminAssistant.DAL.EntityFramework.Model.Grocery
{
    public class GroceryItemEntity
    {
        public int GroceryItemID { get; set; }
        public int AuditID { get; set; }
        public int OwnerID { get; internal set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "EF Core binding a URI that is validated elsewhere.")]
        public string URI { get; set; } = string.Empty;

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
