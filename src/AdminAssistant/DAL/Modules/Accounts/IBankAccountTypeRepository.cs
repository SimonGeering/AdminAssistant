using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public interface IBankAccountTypeRepository
    {
        Task<IList<BankAccountType>> GetBankAccountTypeListAsync();
    }
}
