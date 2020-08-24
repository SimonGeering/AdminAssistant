using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTypeRepository : RepositoryBase, IBankAccountTypeRepository
    {
        public BankAccountTypeRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<BankAccountType> GetAsync(int id)
        {
            var data = await this.DbContext.BankAccountTypes.FirstOrDefaultAsync(x => x.BankAccountTypeID == id).ConfigureAwait(false);
            return this.Mapper.Map<BankAccountType>(data);
        }

        public async Task<List<BankAccountType>> GetListAsync()
        {
            var data = await this.DbContext.BankAccountTypes.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountType>>(data);
        }
    }
}
