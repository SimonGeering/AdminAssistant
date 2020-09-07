using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.DocumentsModule;
using Microsoft.EntityFrameworkCore;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;

namespace AdminAssistant.Infra.DAL.Modules.DocumentsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class DocumentRepository : RepositoryBase, IDocumentRepository
    {
        public DocumentRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Document> GetAsync(int documentID)
        {
            var data = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == documentID).ConfigureAwait(false);
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
                DbContext.Documents.Add(entity);
            else
                DbContext.Documents.Update(entity);

            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return Mapper.Map<Document>(entity);
        }

        public async Task DeleteAsync(int documentID)
        {
            var entity = await DbContext.Documents.FirstOrDefaultAsync(x => x.DocumentID == documentID).ConfigureAwait(false);

            // TODO: make this a custom domain exception and handle in controller.
            if (entity.DocumentID != documentID)
                throw new System.ArgumentException($"Record with ID {documentID} not found", nameof(documentID));

            DbContext.Documents.Remove(entity);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }
    }
}
