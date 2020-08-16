using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IAccountsService
    {
        Task<List<BankAccountType>> LoadBankAccountTypesLookupDataAsync();
        Task<BankAccount> CreateBankAccountAsync(BankAccount model);
        Task<BankAccount> UpdateBankAccountAsync(BankAccount model);
    }
}
