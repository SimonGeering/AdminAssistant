using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

public interface IBankAccountTypeRepository : IReadOnlyRepository<BankAccountType, BankAccountTypeId>
{
}
