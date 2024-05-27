using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

public interface IBankAccountTypeRepository : IReadOnlyRepository<BankAccountType, BankAccountTypeId>;

internal sealed class SqlServerBankAccountTypeRepository(
    SqlServerApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountTypeRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountTypeRepository
{
}

internal sealed class PostgresBankAccountTypeRepository(
    PostgresApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : BankAccountTypeRepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountTypeRepository
{
}

internal abstract class BankAccountTypeRepositoryBase(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, mapper, dateTimeProvider, userContextProvider), IBankAccountTypeRepository
{
    public async Task<BankAccountType?> GetAsync(BankAccountTypeId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTypes.FirstOrDefaultAsync(x => x.BankAccountTypeID == id.Value, cancellationToken).ConfigureAwait(false);
        return Mapper.Map<BankAccountType>(data);
    }

    public async Task<List<BankAccountType>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.BankAccountTypes.ToListAsync(cancellationToken).ConfigureAwait(false);
        return Mapper.Map<List<BankAccountType>>(data);
    }
}
