using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.DocumentsModule
{
    [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class DocumentsService : ServiceBase, IDocumentsService
    {
        public DocumentsService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
            : base(adminAssistantWebAPIClient, mapper, log)
        {
        }

        public async Task<List<Document>> GetDocumentListAsync()
        {
            Log.Start();

            var response = await AdminAssistantWebAPIClient.GetDocumentAsync().ConfigureAwait(false);
            var result = new List<Document>(Mapper.Map<IEnumerable<Document>>(response));

            return Log.Finish(result);
        }
    }
}
