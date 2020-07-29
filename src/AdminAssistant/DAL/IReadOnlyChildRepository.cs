using System.Collections.Generic;
using System.Threading.Tasks;

using AdminAssistant.DomainModel;

namespace AdminAssistant.DAL
{
    public interface IReadOnlyChildRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> GetAsync(int id);
        Task<IList<TDomainModel>> GetListAsync(int parentID);
        Task<IList<TDomainModel>> GetListAsync(IDatabasePersistable parent) => this.GetListAsync(parent.PrimaryKey);
    }
}
