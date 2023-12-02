using AdminAssistant.Infra.DAL;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountTransactionRepository : IReadOnlyChildRepository<BankAccountTransaction>;

internal sealed class BankAccountTransactionRepository(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountTransactionRepository
{
    public async Task<BankAccountTransaction> GetAsync(int id, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTransactions.FirstOrDefaultAsync(x => x.BankAccountTransactionID == id, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<BankAccountTransaction>(data);
    }

    public async Task<List<BankAccountTransaction>> GetListAsync(int parentID, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTransactions.Where(x => x.BankAccountID == parentID).ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<BankAccountTransaction>>(data);
    }
}
