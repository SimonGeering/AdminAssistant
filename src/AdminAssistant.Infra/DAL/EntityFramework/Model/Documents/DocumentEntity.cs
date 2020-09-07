using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Documents
{
    public class DocumentEntity : IMapFrom<Document>, IMapTo<Document>
    {
        public int DocumentID { get; set; }
        public int AuditID { get; internal set; }
        public int OwnerID { get; internal set; }
        [SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "EF Core binding a URI that is validated elsewhere.")]
        public string URI { get; set; } = string.Empty;

        public Core.AuditEntity Audit { get; internal set; } = null!;
    }
}
