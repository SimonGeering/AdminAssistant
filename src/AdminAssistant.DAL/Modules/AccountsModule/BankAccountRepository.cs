using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountRepository : RepositoryBase, IBankAccountRepository
    {
        public BankAccountRepository(IApplicationDbContext dbContext, IMapper mapper)
            :base(dbContext, mapper)
        {
        }

        public async Task<IList<BankAccountInfo>> GetBankAccountInfoListAsync(int ownerID)
        {
            return await this.DbContext.Accounts.BankAccounts.Select(x => new BankAccountInfo
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
            var data = await this.DbContext.Accounts.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == bankAccountID).ConfigureAwait(false);
            return this.Mapper.Map<BankAccount>(data);
        }

        public async Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var source = await this.DbContext.Accounts.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID).ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountTransaction>>(source);
        }

        public async Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount)
        {
            var bankAccountToAdd = Mapper.Map<BankAccountEntity>(bankAccount);
            this.DbContext.Accounts.BankAccounts.Add(bankAccountToAdd);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            return this.Mapper.Map<BankAccount>(bankAccountToAdd);
        }
    }
}
