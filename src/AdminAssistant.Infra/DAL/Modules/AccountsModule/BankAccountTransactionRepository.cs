using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTransactionRepository : RepositoryBase, IBankAccountTransactionRepository
    {
        public BankAccountTransactionRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
            : base(dbContext, mapper, dateTimeProvider, userContextProvider)
        {
        }

        public async Task<BankAccountTransaction> GetAsync(int id)
        {
            var data = await DbContext.BankAccountTransactions.FirstOrDefaultAsync(x => x.BankAccountTransactionID == id).ConfigureAwait(false);
            return Mapper.Map<BankAccountTransaction>(data);
        }

        public async Task<List<BankAccountTransaction>> GetListAsync(int parentID)
        {
            var data = await DbContext.BankAccountTransactions.Where(x => x.BankAccountID == parentID).ToListAsync().ConfigureAwait(false);
            return Mapper.Map<List<BankAccountTransaction>>(data);
        }
    }
}
