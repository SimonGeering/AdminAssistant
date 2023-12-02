namespace AdminAssistant.Infra.DAL;

public interface IRepository<TDomainModel, TId> : IReadOnlyRepository<TDomainModel, TId>
    where TDomainModel : IPersistable
    where TId : Id
{
    Task<TDomainModel> SaveAsync(TDomainModel domainObjectToSave, CancellationToken cancellationToken);
    Task DeleteAsync(TId id, CancellationToken cancellationToken);
}
