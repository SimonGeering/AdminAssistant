using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule
{
    public interface IBankAccountInfoRepository : IReadOnlyRepository<BankAccountInfo>
    {
    }
}
