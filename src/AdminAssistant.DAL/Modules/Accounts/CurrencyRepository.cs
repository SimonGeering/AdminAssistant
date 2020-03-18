using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public CurrencyRepository(IApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IList<Currency>> GetCurrencyListAsync()
        {
            var data = await db.Currencies.ToListAsync().ConfigureAwait(false);
            return this.mapper.Map<List<Currency>>(data);
        }
    }
}
