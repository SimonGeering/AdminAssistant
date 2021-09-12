using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.Infra.Providers;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.CoreModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CurrencyRepository : RepositoryBase, ICurrencyRepository
    {
        public CurrencyRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
            : base(dbContext, mapper, dateTimeProvider, userContextProvider)
        {
        }

        public async Task<Currency> SaveAsync(Currency domainObjectToSave)
        {
            var entity = Mapper.Map<CurrencyEntity>(domainObjectToSave);

            if (base.IsNew(domainObjectToSave))
            {                
                DbContext.Currencies.Add(entity);
            }
            else
            {
                DbContext.Currencies.Update(entity);
            }

            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return Mapper.Map<Currency>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Currencies.FirstOrDefaultAsync(x => x.CurrencyID == id).ConfigureAwait(false);

            // TODO: make this a custom domain exception and handle in controller.
            if (entity == null || entity.CurrencyID != id)
                throw new ArgumentException($"Record with ID {id} not found", nameof(id));

            DbContext.Currencies.Remove(entity);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public async Task<Currency?> GetAsync(int id)
        {
            var data = await DbContext.Currencies.FirstOrDefaultAsync(x => x.CurrencyID == id).ConfigureAwait(false);
            return Mapper.Map<Currency>(data);
        }

        public async Task<List<Currency>> GetListAsync()
        {
            var data = await DbContext.Currencies.ToListAsync().ConfigureAwait(false);
            return Mapper.Map<List<Currency>>(data);
        }
    }
}
