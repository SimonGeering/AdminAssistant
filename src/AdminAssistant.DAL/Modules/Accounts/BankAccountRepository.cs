using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.Accounts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public class BankAccountRepository : RepositoryBase, IBankAccountRepository
    {
        public BankAccountRepository(IApplicationDbContext dbContext, IMapper mapper)
            :base(dbContext, mapper)
        {
        }

        public async Task<IList<BankAccountInfo>> GetBankAccountInfoListAsync(int ownerID)
        {
            return await this.DbContext.BankAccounts.Select(x => new BankAccountInfo
            {
                BankAccountID = x.BankAccountID,
                AccountName = x.AccountName,
                CurrentBalance = x.CurrentBalance,
                IsBudgeted = x.IsBudgeted,
                Symbol = x.Currency.Symbol,
                DecimalFormat = x.Currency.DecimalFormat,
            }).ToListAsync().ConfigureAwait(false);
        }

        public async Task<BankAccount> GetBankAccountAsync(int bankAccountID)
        {
            var data = await this.DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == bankAccountID).ConfigureAwait(false);
            return this.Mapper.Map<BankAccount>(data);
        }

        public async Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var source = await this.DbContext.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID).ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountTransaction>>(source);
        }
    }
}
