using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public class CurrencyRepository : RepositoryBase, ICurrencyRepository
    {
        public CurrencyRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<Currency>> GetCurrencyListAsync()
        {
            var data = await this.DbContext.Currencies.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<Currency>>(data);
        }
    }
}
