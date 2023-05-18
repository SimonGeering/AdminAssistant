using AutoMapper;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;
using AdminAssistant.Infra.Providers;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.DocumentsModule;

internal sealed class DocumentRepository : RepositoryBase, IDocumentRepository
{
    public DocumentRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
        : base(dbContext, mapper, dateTimeProvider, userContextProvider)
    {
    }

    public async Task<Document?> GetAsync(int id)
    {
        var data = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == id).ConfigureAwait(false);
        return Mapper.Map<Document>(data);
    }

    public async Task<List<Document>> GetListAsync()
    {
        var data = await DbContext.Documents.ToListAsync().ConfigureAwait(false);
        return Mapper.Map<List<Document>>(data);
    }

    public async Task<Document> SaveAsync(Document domainObjectToSave)
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

        await DbContext.SaveChangesAsync().ConfigureAwait(false);

        return Mapper.Map<Document>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == id).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.DocumentID != id)
            throw new ArgumentException($"Record with ID {id} not found", nameof(id));

        DbContext.Documents.Remove(entity);
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
        return;
    }
}
