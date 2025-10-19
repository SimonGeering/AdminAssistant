using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.DocumentsModule.Infrastructure.DAL;

public interface IDocumentRepository : IRepository<Document, DocumentId>;

internal sealed class DocumentRepository(
    IApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, dateTimeProvider, userContextProvider), IDocumentRepository
{
    public async Task<Document?> GetAsync(DocumentId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == id.Value, cancellationToken).ConfigureAwait(false);
        return data?.ToDocument();
    }

    public async Task<List<Document>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.Documents.ToListAsync(cancellationToken).ConfigureAwait(false);
        return data.ToDocumentList();
    }

    public async Task<Document> SaveAsync(Document domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = domainObjectToSave.ToDocumentEntity();

        if (IsNew(domainObjectToSave))
        {
            entity.Audit = new AuditEntity()
            {
                CreatedBy = UserContextProvider.GetCurrentUser().SignOn,
                CreatedOn = DateTimeProvider.UtcNow
            };
            DbContext.Documents.Add(entity);
        }
        else
        {
            // TODO: handle audit change
            DbContext.Documents.Update(entity);
        }

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity.ToDocument();
    }

    public async Task DeleteAsync(DocumentId id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == id.Value, cancellationToken).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.DocumentID != id.Value)
            throw new ArgumentException($"Record with ID {id.Value} not found", nameof(id));

        DbContext.Documents.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
