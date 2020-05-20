using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public interface IBankAccountRepository
    {
        Task<IList<BankAccountInfo>> GetBankAccountInfoListAsync(int ownerID);
        Task<BankAccount> GetBankAccountAsync(int bankAccountID);
        Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID);
        Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount);
    }
}
