using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;
using AdminAssistant.Infrastructure.Providers;

namespace AdminAssistant.Modules.CoreModule.Infrastructure.DAL;

public interface ICurrencyRepository : IRepository<Currency, CurrencyId>;

internal sealed class CurrencyRepository(
    IApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, dateTimeProvider, userContextProvider), ICurrencyRepository
{
    public async Task<Currency> SaveAsync(Currency domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = domainObjectToSave.ToCurrencyEntity();

        if (IsNew(domainObjectToSave))
        {
            DbContext.Currencies.Add(entity);
        }
        else
        {
            DbContext.Currencies.Update(entity);
        }

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity.ToCurrency();
    }

    public async Task DeleteAsync(CurrencyId id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Currencies.FirstOrDefaultAsync(x => x.CurrencyID == id.Value, cancellationToken).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.CurrencyID != id.Value)
            throw new ArgumentException($"Record with ID {id.Value} not found", nameof(id));

        DbContext.Currencies.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<Currency?> GetAsync(CurrencyId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.Currencies.FirstOrDefaultAsync(x => x.CurrencyID == id.Value, cancellationToken).ConfigureAwait(false);
        return data?.ToCurrency();
    }

    public async Task<List<Currency>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.Currencies.ToListAsync(cancellationToken).ConfigureAwait(false);
        return data.ToCurrencyList();
    }
}
