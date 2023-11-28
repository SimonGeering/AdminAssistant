namespace AdminAssistant.Infra.DAL;

public interface IReadOnlyRepository<TDomainModel, TId>
    where TDomainModel : IDatabasePersistable
    where TId : Id
{
    Task<TDomainModel?> GetAsync(TId id, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(CancellationToken cancellationToken);
}
