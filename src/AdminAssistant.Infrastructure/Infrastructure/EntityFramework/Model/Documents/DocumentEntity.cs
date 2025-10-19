namespace AdminAssistant.Infrastructure.EntityFramework.Model.Documents;

public sealed class DocumentEntity
{
    public int DocumentID { get; set; }
    public int AuditID { get; internal set; }
    public int OwnerID { get; internal set; }
    public string FileName { get; set; } = string.Empty;

    public Core.AuditEntity Audit { get; internal set; } = null!;
}
