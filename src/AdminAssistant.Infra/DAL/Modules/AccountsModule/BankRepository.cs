using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.Providers;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankRepository : RepositoryBase, IBankRepository
    {
        public BankRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
            : base(dbContext, mapper, dateTimeProvider, userContextProvider)
        {
        }

        public async Task<Bank?> GetAsync(int bankID)
        {
            var data = await DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == bankID).ConfigureAwait(false);
            return Mapper.Map<Bank>(data);
        }

        public async Task<List<Bank>> GetListAsync()
        {
            var data = await DbContext.Banks.ToListAsync().ConfigureAwait(false);
            return Mapper.Map<List<Bank>>(data);
        }

        public async Task<Bank> SaveAsync(Bank domainObjectToSave)
        {
            var entity = Mapper.Map<BankEntity>(domainObjectToSave);

            if (base.IsNew(domainObjectToSave))
                DbContext.Banks.Add(entity);
            else
                DbContext.Banks.Update(entity);

            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return Mapper.Map<Bank>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == id).ConfigureAwait(false);

            // TODO: make this a custom domain exception and handle in controller.
            if (entity == null || entity.BankID != id)
                throw new ArgumentException($"Record with ID {id} not found", nameof(id));

            DbContext.Banks.Remove(entity);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            return;
        }
    }
}
