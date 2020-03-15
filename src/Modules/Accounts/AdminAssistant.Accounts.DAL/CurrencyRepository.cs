using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.Accounts.DAL.EntityFramework;
using AdminAssistant.Accounts.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Accounts.DAL
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
