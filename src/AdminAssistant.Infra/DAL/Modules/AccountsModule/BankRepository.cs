using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankRepository : RepositoryBase, IBankRepository
    {
        public BankRepository(IApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Bank> GetAsync(int bankID)
        {
            var data = await this.DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == bankID).ConfigureAwait(false);
            return this.Mapper.Map<Bank>(data);
        }

        public async Task<List<Bank>> GetListAsync()
        {
            var data = await this.DbContext.Banks.ToListAsync().ConfigureAwait(false);
            return this.Mapper.Map<List<Bank>>(data);
        }

        public async Task<Bank> SaveAsync(Bank domainObjectToSave)
        {
            var entity = Mapper.Map<BankEntity>(domainObjectToSave);

            if (base.IsNew(domainObjectToSave))
                this.DbContext.Banks.Add(entity);
            else
                this.DbContext.Banks.Update(entity);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            return this.Mapper.Map<Bank>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == id).ConfigureAwait(false);

            // TODO: make this a custom domain exception and handle in controller.
            if (entity.BankID != id)
                throw new System.ArgumentException($"Record with ID {id} not found", nameof(id));

            this.DbContext.Banks.Remove(entity);
            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }
    }
}
