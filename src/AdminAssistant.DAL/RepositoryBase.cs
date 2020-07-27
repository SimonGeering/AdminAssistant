using AdminAssistant.DAL.EntityFramework;
using AutoMapper;

namespace AdminAssistant.DAL
{
    internal abstract class RepositoryBase
    {
        protected IApplicationDbContext DbContext { get; }
        protected IMapper Mapper { get; }

        public RepositoryBase(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }
    }
}
