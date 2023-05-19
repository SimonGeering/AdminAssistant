namespace AdminAssistant.Infra.DAL.EntityFramework.Model.AssetRegister;

public sealed class AssetEntity
{
    // Table "Assets.Asset"
    public int AssetID { get; set; } // PK
    public int ManufacturerID { get; set; }
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }
    public int PurchasePrice { get; set; }
    public int DepreciatedValue { get; set; }
    public int ReplacementCost { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;
}
