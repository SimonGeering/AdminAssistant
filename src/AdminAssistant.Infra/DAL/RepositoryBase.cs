using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.Providers;
using AutoMapper;

namespace AdminAssistant.Infra.DAL;

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

    public bool IsNew(IDatabasePersistable domainObject) => domainObject.IsNew;
}
