using AdminAssistant.Infra.DAL.EntityFramework;
using AutoMapper;

namespace AdminAssistant.Infra.DAL
{
    internal abstract class RepositoryBase
    {
        protected IApplicationDbContext DbContext { get; }
        protected IMapper Mapper { get; }

        public RepositoryBase(IApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
        public bool IsNew(IDatabasePersistable domainObject) => domainObject.IsNew;
    }
}
