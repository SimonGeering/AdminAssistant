namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Grocery;

public sealed class GroceryItemEntity
{
    public int GroceryItemID { get; set; }
    public int AuditID { get; set; }
    public int OwnerID { get; internal set; }


    public string URI { get; set; } = string.Empty;

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
