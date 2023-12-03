namespace AdminAssistant.Modules.DocumentsModule.UI;

public interface IDocumentsService
{
    Task<List<Document>> GetDocumentListAsync();
}
internal sealed class DocumentsService(
    IAdminAssistantWebAPIClient adminAssistantWebAPIClient,
    IMapper mapper,
    ILoggingProvider log)
    : ServiceBase(adminAssistantWebAPIClient, mapper, log), IDocumentsService
{
    public async Task<List<Document>> GetDocumentListAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetDocumentAsync().ConfigureAwait(false);
        var result = new List<Document>(Mapper.Map<IEnumerable<Document>>(response));

        return Log.Finish(result);
    }
}
