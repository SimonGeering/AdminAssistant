using AutoMapper;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;
using AdminAssistant.Infra.Providers;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.DocumentsModule;

internal sealed class DocumentRepository(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IDocumentRepository
{
    public async Task<Document?> GetAsync(DocumentId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == id.Value, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<Document>(data);
    }

    public async Task<List<Document>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.Documents.ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<Document>>(data);
    }

    public async Task<Document> SaveAsync(Document domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<DocumentEntity>(domainObjectToSave);

        if (base.IsNew(domainObjectToSave))
        {
            entity.Audit = new EntityFramework.Model.Core.AuditEntity()
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

        return Mapper.Map<Document>(entity);
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
