using AutoMapper;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

internal sealed class BankAccountTypeRepository(
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
