using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.DocumentsModule;

namespace AdminAssistant.UI.Modules.DocumentsModule
{
    public interface IDocumentsService
    {
        Task<List<Document>> GetDocumentListAsync();
    }
}
