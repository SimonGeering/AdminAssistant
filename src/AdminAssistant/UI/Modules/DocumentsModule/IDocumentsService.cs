using AdminAssistant.DomainModel.Modules.DocumentsModule;

namespace AdminAssistant.UI.Modules.DocumentsModule
{
    public interface IDocumentsService
    {
        Task<List<Document>> GetDocumentListAsync();
    }
}
