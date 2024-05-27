using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountRepository : IRepository<BankAccount, BankAccountId>;

internal sealed class SqlServerBankAccountRepository(
    SqlServerApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider)
{
}

internal sealed class PostgresBankAccountRepository(
    PostgresApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider)
{
}

internal abstract class BankAccountRepositoryBase(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountRepository
{
    public async Task<List<BankAccountTransaction>> GetBankAccountTransactionListAsync(BankAccountId bankAccountID, CancellationToken cancellationToken)
    {
        var source = await DbContext.BankAccountTransactions.Where(x => x.BankAccountID == bankAccountID.Value).ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<BankAccountTransaction>>(source);
    }

    public async Task<BankAccount> SaveAsync(BankAccount domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<BankAccountEntity>(domainObjectToSave);

        if (IsNew(domainObjectToSave))
        {
            entity.Audit = new AuditEntity()
            {
                CreatedBy = UserContextProvider.GetCurrentUser().SignOn,
                CreatedOn = DateTimeProvider.UtcNow
            };
            DbContext.BankAccounts.Add(entity);
        }
        else
        {
            entity.Audit = await DbContext.AuditTrail.SingleAsync(x => x.AuditID == entity.AuditID, cancellationToken);
            entity.Audit.UpdatedBy = UserContextProvider.GetCurrentUser().SignOn;
            entity.Audit.UpdatedOn = DateTimeProvider.UtcNow;

            DbContext.BankAccounts.Update(entity);
        }

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Mapper.Map<BankAccount>(entity);
    }

    public async Task DeleteAsync(BankAccountId id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id.Value, cancellationToken).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.BankAccountID != id.Value)
            throw new ArgumentException($"Record with ID {id.Value} not found", nameof(id));

        DbContext.BankAccounts.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<BankAccount?> GetAsync(BankAccountId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountID == id.Value, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<BankAccount?>(data);
    }

    public async Task<List<BankAccount>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccounts.ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<BankAccount>>(data);
    }
}
