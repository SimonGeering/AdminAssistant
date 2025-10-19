using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;

namespace AdminAssistant.Infrastructure.DAL;

internal abstract class RepositoryBase
{
    protected IApplicationDbContext DbContext { get; }
    protected IDateTimeProvider DateTimeProvider { get; }
    protected IUserContextProvider UserContextProvider { get; }

    protected RepositoryBase(IApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
    {
        DbContext = dbContext;
        DateTimeProvider = dateTimeProvider;
        UserContextProvider = userContextProvider;
    }

    protected static bool IsNew(IPersistable domainObject) => domainObject.IsNew;
}
