namespace AdminAssistant.Infra.DAL
{
    public interface IRepository<TDomainModel> : IReadOnlyRepository<TDomainModel>
        where TDomainModel : IDatabasePersistable
    {
        Task<TDomainModel> SaveAsync(TDomainModel domainObjectToSave);
        Task DeleteAsync(int id);
    }
}
