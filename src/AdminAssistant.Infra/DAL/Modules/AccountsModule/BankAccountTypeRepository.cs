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
    public async Task<BankAccountType?> GetAsync(int id)
    {
        var data = await DbContext.BankAccountTypes.FirstOrDefaultAsync(x => x.BankAccountTypeID == id).ConfigureAwait(false);
        return Mapper.Map<BankAccountType>(data);
    }

    public async Task<List<BankAccountType>> GetListAsync()
    {
        var data = await DbContext.BankAccountTypes.ToListAsync().ConfigureAwait(false);
        return Mapper.Map<List<BankAccountType>>(data);
    }
}
