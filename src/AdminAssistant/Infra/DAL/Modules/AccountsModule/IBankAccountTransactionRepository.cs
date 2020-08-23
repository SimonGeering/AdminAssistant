using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    public interface IBankAccountTransactionRepository : IReadOnlyChildRepository<BankAccountTransaction>
    {
    }
}
