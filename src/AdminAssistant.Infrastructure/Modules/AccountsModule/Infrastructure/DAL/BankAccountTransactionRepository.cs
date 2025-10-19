using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountTransactionRepository : IReadOnlyChildRepository<BankAccountTransaction>;

internal sealed class BankAccountTransactionRepository(
    IApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, dateTimeProvider, userContextProvider), IBankAccountTransactionRepository
{
    public async Task<BankAccountTransaction?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTransactions.FirstOrDefaultAsync(x => x.BankAccountTransactionID == id, cancellationToken).ConfigureAwait(false);
        return data?.ToBankAccountTransaction();
    }

    public async Task<List<BankAccountTransaction>> GetListAsync(int parentID, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTransactions.Where(x => x.BankAccountID == parentID).ToListAsync(cancellationToken).ConfigureAwait(false);
        return data.ToBankAccountTransactionList();
    }
}
