using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountTransactionRepository : IReadOnlyChildRepository<BankAccountTransaction>;

internal sealed class SqlServerBankAccountTransactionRepository(
    SqlServerApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountTransactionRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider)
{
}

internal sealed class PostgresBankAccountTransactionRepository(
    PostgresApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountTransactionRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider)
{
}

internal abstract class BankAccountTransactionRepositoryBase(
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
