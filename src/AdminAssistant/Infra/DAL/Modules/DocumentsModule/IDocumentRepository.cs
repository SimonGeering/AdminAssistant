using AdminAssistant.DomainModel.Modules.DocumentsModule;

namespace AdminAssistant.Infra.DAL.Modules.DocumentsModule
{
    public interface IDocumentRepository : IReadOnlyRepository<Document>
    {
    }
}
