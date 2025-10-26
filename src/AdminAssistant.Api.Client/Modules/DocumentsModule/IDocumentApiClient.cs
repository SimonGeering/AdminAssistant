using AdminAssistant.WebAPI.v1.DocumentsModule;

namespace AdminAssistant.WebAPIClient.v1.DocumentsModule;

public interface IDocumentApiClient
{
    [Get("/api/v1/document-module/Document")]
    Task<IEnumerable<DocumentResponseDto>> GetDocumentsAsync(CancellationToken cancellationToken = default);
}
