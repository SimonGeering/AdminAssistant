using AdminAssistant.WebAPI.v1.DocumentsModule;

namespace AdminAssistant.Modules.DocumentsModule;

internal static class DocumentsModuleMapper
{
    private static Document ToDocument(this DocumentResponseDto source)
        => new()
        {
            DocumentID = new DocumentId(source.DocumentID),
            FileName = source.FileName
        };

    internal static IEnumerable<Document> ToDocumentEnumeration(this IEnumerable<DocumentResponseDto> source)
        => source.Select(ToDocument);
}

