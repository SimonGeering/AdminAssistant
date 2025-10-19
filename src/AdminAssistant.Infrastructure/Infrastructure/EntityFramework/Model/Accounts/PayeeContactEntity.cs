namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class PayeeContactEntity
{
    // Table "Accounts.PayeeContact"
    public int PayeeContactID { get; set; } // PK
    public int PayeeID { get; set; }
    public int ContactID { get; set; }
    public int AuditID { get; set; }
    public bool IsPrimaryContact { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
