using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CurrencyRepository : RepositoryBase, ICurrencyRepository
    {
        public CurrencyRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<Currency>> GetCurrencyListAsync()
        {
            var data = await this.DbContext.Core.Currencies.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<Currency>>(data);
        }
    }
}
