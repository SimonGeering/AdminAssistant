namespace AdminAssistant.Infra.DAL
{
    public interface IReadOnlyChildRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> GetAsync(int id);
        Task<List<TDomainModel>> GetListAsync(int parentID);
        Task<List<TDomainModel>> GetListAsync(IDatabasePersistable parent) => GetListAsync(parent.PrimaryKey);
    }
}
