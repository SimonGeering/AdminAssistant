using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountInfoRepository : IReadOnlyRepository<BankAccountInfo, BankId>;

internal sealed class BankAccountInfoRepository(
    ApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountInfoRepository
{

    // TODO: Check this transform is being done in TSQL server side.
    public async Task<BankAccountInfo?> GetAsync(BankId id, CancellationToken cancellationToken)
        => await DbContext.BankAccounts.Select(x => new BankAccountInfo
        {
            BankAccountID = new(x.BankAccountID),
            AccountName = x.AccountName,
            CurrentBalance = x.CurrentBalance,
            IsBudgeted = x.IsBudgeted,
            Symbol = x.Currency.Symbol,
            DecimalFormat = x.Currency.DecimalFormat,
        }).FirstOrDefaultAsync(x => x.BankAccountID.Value == id.Value, cancellationToken).ConfigureAwait(false);

    // TODO: Check this transform is being done in TSQL server side.
    public async Task<List<BankAccountInfo>> GetListAsync(CancellationToken cancellationToken)
        => await DbContext.BankAccounts.Select(x => new BankAccountInfo
        {
            BankAccountID = new(x.BankAccountID),
            AccountName = x.AccountName,
            CurrentBalance = x.CurrentBalance,
            IsBudgeted = x.IsBudgeted,
            Symbol = x.Currency.Symbol,
            DecimalFormat = x.Currency.DecimalFormat,
        }).ToListAsync(cancellationToken).ConfigureAwait(false);
}
