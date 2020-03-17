using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public class BankAccountTypeRepository : IBankAccountTypeRepository
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public BankAccountTypeRepository(IApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IList<BankAccountType>> GetBankAccountTypeListAsync()
        {
            var data = await db.BankAccountTypes.ToListAsync().ConfigureAwait(false);
            return this.mapper.Map<List<BankAccountType>>(data);
        }
    }
}
