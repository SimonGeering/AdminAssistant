namespace AdminAssistant.Infra.DAL.EntityFramework.Model.AssetRegister;

public sealed class ManufacturerEntity
{
    // Table "Assets.Manufacturer"
    public int ManufacturerID { get; set; } // PK
    public string ManufacturerName { get; set; } = string.Empty;
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    // Ref: "Assets.Manufacturer"."ManufacturerID" < "Assets.Asset"."ManufacturerID"
}
