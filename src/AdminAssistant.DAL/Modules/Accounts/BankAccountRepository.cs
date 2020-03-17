using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly IApplicationDbContext db;
        private readonly IMapper mapper;

        public BankAccountRepository(IApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IList<BankAccountInfo>> GetBankAccountInfoListAsync(int ownerID)
        {
            return await db.BankAccounts.Select(x => new BankAccountInfo
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
            var data = await db.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == bankAccountID).ConfigureAwait(false);
            return this.mapper.Map<BankAccount>(data);
        }

        public async Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var source = await db.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID).ToListAsync().ConfigureAwait(false);
            return this.mapper.Map<List<BankAccountTransaction>>(source);
        }
    }
}
