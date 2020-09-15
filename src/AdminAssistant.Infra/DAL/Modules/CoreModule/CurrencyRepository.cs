using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
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

        public async Task<Currency> GetAsync(int id)
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
