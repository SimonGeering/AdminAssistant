using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminAssistant.Infra.DAL
{
    public interface IReadOnlyRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> GetAsync(int id);
        Task<List<TDomainModel>> GetListAsync();
    }
}
