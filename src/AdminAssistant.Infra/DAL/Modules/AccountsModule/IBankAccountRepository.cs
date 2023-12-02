using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

public interface IBankAccountRepository : IRepository<BankAccount, BankAccountId>
{
}
