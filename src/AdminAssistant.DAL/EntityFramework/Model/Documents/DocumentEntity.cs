namespace AdminAssistant.DAL.EntityFramework.Model.Documents
{
    public class DocumentEntity
    {
        public int DocumentID { get; set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
