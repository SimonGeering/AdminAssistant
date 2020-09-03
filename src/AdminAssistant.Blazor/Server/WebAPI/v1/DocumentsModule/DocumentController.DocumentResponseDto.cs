using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.DocumentsModule
{
    public class DocumentResponseDto : IMapFrom<Document>
    {
        public int DocumentID { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
