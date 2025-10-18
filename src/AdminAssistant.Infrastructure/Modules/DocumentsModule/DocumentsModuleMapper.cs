using AdminAssistant.Infrastructure.EntityFramework.Model.Documents;

namespace AdminAssistant.Modules.DocumentsModule;

public static class DocumentsModuleMapper
{
    public static Document ToDocument(this DocumentEntity entity)
        => new()
        {
            DocumentID = new DocumentId(entity.DocumentID),
            FileName = entity.FileName,
        };

    public static List<Document> ToDocumentList(this List<DocumentEntity> entities)
        => entities.Select(ToDocument).ToList();

    public static DocumentEntity ToDocumentEntity(this Document domainObject)
        => new()
        {
            DocumentID = domainObject.DocumentID.Value,
            FileName = domainObject.FileName,
        };
}
