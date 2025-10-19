namespace AdminAssistant.Modules.DocumentsModule;

internal static class DocumentsModuleMapper
{
    private static Document ToDocument(this DocumentResponseDto source)
        => new()
        {
            DocumentID = source.DocumentID.HasValue ? new DocumentId(source.DocumentID.Value) : DocumentId.Default,
            FileName = source.FileName ?? string.Empty,
        };

    internal static IEnumerable<Document> ToDocumentEnumeration(this ICollection<DocumentResponseDto> source)
        => source.Select(ToDocument);
}

