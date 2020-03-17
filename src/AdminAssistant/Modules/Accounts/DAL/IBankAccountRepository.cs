using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DomainModel;

namespace AdminAssistant.Accounts.DAL
{
    public interface IBankAccountRepository
    {
        Task<IList<BankAccountInfo>> GetBankAccountInfoListAsync(int ownerID);
        Task<BankAccount> GetBankAccountAsync(int bankAccountID);
        Task<IList<BankAccountTransaction>> GetBankAccountTransactionListAsync(int bankAccountID);
    }
}
