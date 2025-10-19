namespace AdminAssistant.Modules.DocumentsModule.UI;

public interface IDocumentsService
{
    Task<List<Document>> GetDocumentListAsync();
}
internal sealed class DocumentsService(
    IAdminAssistantWebAPIClient adminAssistantWebApiClient,
    ILoggingProvider log)
    : ServiceBase(adminAssistantWebApiClient, log), IDocumentsService
{
    public async Task<List<Document>> GetDocumentListAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetDocumentAsync().ConfigureAwait(false);
        var result = new List<Document>(response.ToDocumentEnumeration());

        return Log.Finish(result);
    }
}
