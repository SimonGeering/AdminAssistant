using System.Threading.Tasks;

using AdminAssistant.DomainModel;

namespace AdminAssistant.DAL
{
    public interface IRepository<TDomainModel> : IReadOnlyRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> SaveAsync(TDomainModel domainObjectToSave);
        Task DeleteAsync(int id);
    }
}
