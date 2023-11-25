namespace AdminAssistant.Infra.DAL;

public interface IReadOnlyRepository<TDomainModel>
    where TDomainModel : IDatabasePersistable
{
    Task<TDomainModel?> GetAsync(int id, CancellationToken cancellationToken);
    Task<List<TDomainModel>> GetListAsync(CancellationToken cancellationToken);
}
