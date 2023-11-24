using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

internal sealed class BankAccountRepository(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountRepository
{
    public async Task<List<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID)
    {
        var source = await DbContext.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID).ToListAsync().ConfigureAwait(false);
        return Mapper.Map<List<BankAccountTransaction>>(source);
    }

    public async Task<BankAccount> SaveAsync(BankAccount domainObjectToSave)
    {
        var entity = Mapper.Map<BankAccountEntity>(domainObjectToSave);

        if (base.IsNew(domainObjectToSave))
        {
            entity.Audit = new EntityFramework.Model.Core.AuditEntity()
            {
                CreatedBy = UserContextProvider.GetCurrentUser().SignOn,
                CreatedOn = DateTimeProvider.UtcNow
            };
            DbContext.BankAccounts.Add(entity);
        }
        else
        {
            entity.Audit = DbContext.AuditTrail.Single(x => x.AuditID == entity.AuditID);
            entity.Audit.UpdatedBy = UserContextProvider.GetCurrentUser().SignOn;
            entity.Audit.UpdatedOn = DateTimeProvider.UtcNow;

            DbContext.BankAccounts.Update(entity);
        }

        await DbContext.SaveChangesAsync().ConfigureAwait(false);

        return Mapper.Map<BankAccount>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.BankAccountID != id)
            throw new ArgumentException($"Record with ID {id} not found", nameof(id));

        DbContext.BankAccounts.Remove(entity);
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<BankAccount?> GetAsync(int id)
    {
        var data = await DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id).ConfigureAwait(false);
        return Mapper.Map<BankAccount?>(data);
    }

    public async Task<List<BankAccount>> GetListAsync()
    {
        var data = await DbContext.BankAccounts.ToListAsync().ConfigureAwait(false);
        return Mapper.Map<List<BankAccount>>(data);
    }
}
