namespace AdminAssistant.Infrastructure.EntityFramework.Model.Billing;

public sealed class ClientEntity
{
    // Table "Billing.Client"
    public int ClientID { get; set; } // PK
    public string ClientName { get; set; } = string.Empty;
    public int AuditID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
