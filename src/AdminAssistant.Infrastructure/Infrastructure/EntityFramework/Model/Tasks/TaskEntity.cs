namespace AdminAssistant.Infrastructure.EntityFramework.Model.Tasks;

public sealed class TaskEntity
{
    // Table "Tasks.Task"
    public int TaskID { get; set; } // PK
    public int AuditID { get; set; }
    public int OwnerID { get; internal set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;
}
