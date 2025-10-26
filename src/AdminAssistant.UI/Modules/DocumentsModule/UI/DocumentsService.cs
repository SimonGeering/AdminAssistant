using AdminAssistant.WebAPIClient.v1.DocumentsModule;

namespace AdminAssistant.Modules.DocumentsModule.UI;

public interface IDocumentsService
{
    Task<List<Document>> GetDocumentListAsync();
}
internal sealed class DocumentsService(
    IDocumentApiClient documentApiClient,
    ILoggingProvider log)
    : ServiceBase(log), IDocumentsService
{
    public async Task<List<Document>> GetDocumentListAsync()
    {
        Log.Start();

        var response = await documentApiClient.GetDocumentsAsync().ConfigureAwait(false);
        var result = new List<Document>(response.ToDocumentEnumeration());

        return Log.Finish(result);
    }
}
