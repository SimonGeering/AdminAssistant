using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

public sealed record DocumentResponseDto : IMapFrom<Document>
{
    public int DocumentID { get; init; }
    public string FileName { get; init; } = string.Empty;
}
