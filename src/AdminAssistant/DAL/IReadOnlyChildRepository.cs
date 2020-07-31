using System.Collections.Generic;
using System.Threading.Tasks;

using AdminAssistant.DomainModel;

namespace AdminAssistant.DAL
{
    public interface IReadOnlyChildRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> GetAsync(int id);
        Task<List<TDomainModel>> GetListAsync(int parentID);
        Task<List<TDomainModel>> GetListAsync(IDatabasePersistable parent) => this.GetListAsync(parent.PrimaryKey);
    }
}
