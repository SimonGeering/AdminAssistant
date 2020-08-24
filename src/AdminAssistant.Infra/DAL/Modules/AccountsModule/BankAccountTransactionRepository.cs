using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTransactionRepository : RepositoryBase, IBankAccountTransactionRepository
    {
        public BankAccountTransactionRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<BankAccountTransaction> GetAsync(int id)
        {
            var data = await this.DbContext.BankAccountTransactions.FirstOrDefaultAsync(x => x.BankAccountTransactionID == id).ConfigureAwait(false);
            return this.Mapper.Map<BankAccountTransaction>(data);
        }

        public async Task<List<BankAccountTransaction>> GetListAsync(int parentID)
        {
            var data = await this.DbContext.BankAccountTransactions.Where(x => x.BankAccountID == parentID).ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountTransaction>>(data);
        }
    }
}
