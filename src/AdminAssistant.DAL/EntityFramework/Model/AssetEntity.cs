namespace AdminAssistant.DAL.EntityFramework.Model
{
    public class AssetEntity
    {
        public int AssetID { get; set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }
        public int PurchasePrice { get; set; }
        public int DepreciatedValue { get; set; }
        public int ReplacementCost { get; set; }

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
