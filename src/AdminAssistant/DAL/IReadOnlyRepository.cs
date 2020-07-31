using System.Collections.Generic;
using System.Threading.Tasks;

using AdminAssistant.DomainModel;

namespace AdminAssistant.DAL
{
    public interface IReadOnlyRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> GetAsync(int id);
        Task<List<TDomainModel>> GetListAsync();
    }
}
