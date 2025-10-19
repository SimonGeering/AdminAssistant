using AdminAssistant.Modules.DocumentsModule;

namespace AdminAssistant.WebAPI.v1.DocumentsModule;

public static class DocumentsModuleMapper
{
    public static IEnumerable<DocumentResponseDto> ToDocumentResponseDtoEnumeration(this IEnumerable<Document> source)
        => source.Select(x => new DocumentResponseDto
        {
            DocumentID = x.DocumentID.Value,
            FileName = x.FileName
        });
}
