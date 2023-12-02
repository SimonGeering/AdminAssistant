using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

internal sealed class BankRepository(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankRepository
{
    public async Task<Bank?> GetAsync(BankId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == id.Value, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<Bank>(data);
    }

    public async Task<List<Bank>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.Banks.ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<Bank>>(data);
    }

    public async Task<Bank> SaveAsync(Bank domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<BankEntity>(domainObjectToSave);

        if (IsNew(domainObjectToSave))
            DbContext.Banks.Add(entity);
        else
            DbContext.Banks.Update(entity);

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Mapper.Map<Bank>(entity);
    }

    public async Task DeleteAsync(BankId id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Banks.FirstOrDefaultAsync(x => x.BankID == id.Value, cancellationToken).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.BankID != id.Value)
            throw new ArgumentException($"Record with ID {id.Value} not found", nameof(id));

        DbContext.Banks.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
