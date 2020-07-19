using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    public class BankAccountTypeRepository : RepositoryBase, IBankAccountTypeRepository
    {
        public BankAccountTypeRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<BankAccountType>> GetBankAccountTypeListAsync()
        {
            var data = await this.DbContext.BankAccountTypes.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountType>>(data);
        }
    }
}
