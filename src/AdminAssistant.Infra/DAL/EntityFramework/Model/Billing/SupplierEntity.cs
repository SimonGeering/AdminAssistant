namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Billing;

public sealed class SupplierEntity
{
    // Table "Billing.SupplierEntity"
    public int SupplierID { get; set; } // PK
    public string SupplierName { get; set; } = string.Empty;
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
