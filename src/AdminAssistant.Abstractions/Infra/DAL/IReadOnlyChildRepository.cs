namespace AdminAssistant.Infra.DAL;

public interface IReadOnlyChildRepository<TDomainModel>
    where TDomainModel : IDatabasePersistable
{
    Task<TDomainModel> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(int parentID, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(IDatabasePersistable parent, CancellationToken cancellationToken)
        => GetListAsync(parent.PrimaryKey, cancellationToken);
}
