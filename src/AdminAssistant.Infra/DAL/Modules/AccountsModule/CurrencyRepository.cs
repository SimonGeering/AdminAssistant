using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CurrencyRepository : RepositoryBase, ICurrencyRepository
    {
        public CurrencyRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Currency> GetAsync(int id)
        {
            var data = await this.DbContext.Currencies.FirstOrDefaultAsync(x => x.CurrencyID == id).ConfigureAwait(false);
            return this.Mapper.Map<Currency>(data);
        }

        public async Task<List<Currency>> GetListAsync()
        {
            var data = await this.DbContext.Currencies.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<Currency>>(data);
        }
    }
}
