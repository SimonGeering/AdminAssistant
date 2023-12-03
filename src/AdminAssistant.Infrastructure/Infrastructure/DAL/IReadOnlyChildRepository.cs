namespace AdminAssistant.Infrastructure.DAL;

public interface IReadOnlyChildRepository<TDomainModel>
    where TDomainModel : IPersistable
{
    Task<TDomainModel> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(int parentID, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(IPersistable parent, CancellationToken cancellationToken)
        => GetListAsync(parent.PrimaryKey.Value, cancellationToken);
}
