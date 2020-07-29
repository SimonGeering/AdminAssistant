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

        public async Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID)
        {
            var source = await this.DbContext.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID).ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccountTransaction>>(source);
        }

        public async Task<BankAccount> SaveAsync(BankAccount domainObjectToSave)
        {
            var entity = Mapper.Map<BankAccountEntity>(domainObjectToSave);

            if (base.IsNew(domainObjectToSave))
                this.DbContext.BankAccounts.Add(entity);
            else
                this.DbContext.BankAccounts.Update(entity);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            return this.Mapper.Map<BankAccount>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id).ConfigureAwait(false);

            // TODO: make this a custom domain exception and handle in controller.
            if (entity.BankAccountID != id)
                throw new System.ArgumentException($"Record with ID {id} not found", nameof(id));

            this.DbContext.BankAccounts.Remove(entity);
            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }

        public async Task<BankAccount> GetAsync(int id)
        {
            var data = await this.DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id).ConfigureAwait(false);
            return this.Mapper.Map<BankAccount>(data);
        }

        public async Task<IList<BankAccount>> GetListAsync()
        {
            var data = await this.DbContext.BankAccounts.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<BankAccount>>(data);
        }
    }
}
