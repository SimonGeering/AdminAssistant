/*
Table "Assets.Asset" 
{
  "AssetID" INT [pk]
  "ManufacturerID" INT [null]
  "AuditID" INT [not null]
  "OwnerID" INT [not null]
  "PurchasePrice" INT
  "DepreciatedValue" INT
  "ReplacementCost" INT
}
*/
namespace AdminAssistant.Infra.DAL.EntityFramework.Model.AssetRegister;

public class AssetEntity
{
    public int AssetID { get; set; }
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }
    public int PurchasePrice { get; set; }
    public int DepreciatedValue { get; set; }
    public int ReplacementCost { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
