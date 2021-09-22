using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

internal class BankAccountInfoRepository : RepositoryBase, IBankAccountInfoRepository
{
    public BankAccountInfoRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
        : base(dbContext, mapper, dateTimeProvider, userContextProvider)
    {
    }

    // TODO: Check this transform is being done in TSQL server side.
    public async Task<BankAccountInfo?> GetAsync(int id) => await DbContext.BankAccounts.Select(x => new BankAccountInfo
    {
        BankAccountID = x.BankAccountID,
        AccountName = x.AccountName,
        CurrentBalance = x.CurrentBalance,
        IsBudgeted = x.IsBudgeted,
        Symbol = x.Currency.Symbol,
        DecimalFormat = x.Currency.DecimalFormat,
    }).FirstOrDefaultAsync(x => x.BankAccountID == id).ConfigureAwait(false);

    // TODO: Check this transform is being done in TSQL server side.
    public async Task<List<BankAccountInfo>> GetListAsync() => await DbContext.BankAccounts.Select(x => new BankAccountInfo
    {
        BankAccountID = x.BankAccountID,
        AccountName = x.AccountName,
        CurrentBalance = x.CurrentBalance,
        IsBudgeted = x.IsBudgeted,
        Symbol = x.Currency.Symbol,
        DecimalFormat = x.Currency.DecimalFormat,
    }).ToListAsync().ConfigureAwait(false);
}
