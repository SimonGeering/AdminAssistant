using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTypeRepository : RepositoryBase, IBankAccountTypeRepository
    {
        public BankAccountTypeRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<BankAccountType>> GetBankAccountTypeListAsync()
        {
            var data = await this.DbContext.Accounts.BankAccountTypes.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountType>>(data);
        }
    }
}
