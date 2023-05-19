namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Billing;

public sealed class InvoiceEntity
{
    // Table "Billing.InvoiceEntity"
    public int InvoiceID { get; set; } // PK
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;
}
