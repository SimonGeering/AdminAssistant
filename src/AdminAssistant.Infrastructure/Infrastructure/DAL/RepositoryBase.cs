using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;

namespace AdminAssistant.Infrastructure.DAL;

internal abstract class RepositoryBase
{
    protected IApplicationDbContext DbContext { get; }
    protected IMapper Mapper { get; }
    protected IDateTimeProvider DateTimeProvider { get; }
    protected IUserContextProvider UserContextProvider { get; }

    protected RepositoryBase(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
    {
        DbContext = dbContext;
        Mapper = mapper;
        DateTimeProvider = dateTimeProvider;
        UserContextProvider = userContextProvider;
    }

    protected static bool IsNew(IPersistable domainObject) => domainObject.IsNew;
}
