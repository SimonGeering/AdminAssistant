namespace AdminAssistant.Infrastructure.DAL;

public interface IReadOnlyRepository<TDomainModel, TId>
    where TDomainModel : IPersistable
    where TId : Id
{
    Task<TDomainModel?> GetAsync(TId id, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(CancellationToken cancellationToken);
}
