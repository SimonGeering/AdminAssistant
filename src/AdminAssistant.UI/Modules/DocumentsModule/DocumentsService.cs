using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.DocumentsModule;

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
