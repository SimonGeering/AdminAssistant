using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

public interface IBankAccountInfoRepository : IReadOnlyRepository<BankAccountInfo, BankId>
{
}
