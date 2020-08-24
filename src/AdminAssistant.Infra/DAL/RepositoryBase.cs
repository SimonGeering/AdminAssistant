using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;

using AutoMapper;

using System;

namespace AdminAssistant.Infra.DAL
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
        public bool IsNew(IDatabasePersistable domainObject) => domainObject.IsNew;
    }
}
