using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.DAL.Modules.AccountsModule
{
    public interface IBankAccountTypeRepository
    {
        Task<IList<BankAccountType>> GetBankAccountTypeListAsync();
    }
}
