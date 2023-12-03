using AdminAssistant.Modules.DocumentsModule;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

public sealed record DocumentResponseDto : IMapFrom<Document>
{
    public int DocumentID { get; init; }
    public string FileName { get; init; } = string.Empty;
}
