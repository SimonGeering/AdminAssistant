using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public interface IAccountsService
    {
        Task<IList<BankAccountType>> LoadBankAccountTypesLookupDataAsync();
        Task<IList<Currency>> LoadCurrencyLookupDataAsync();
        Task<BankAccount> CreateBankAccountAsync(BankAccount model);
        Task<BankAccount> UpdateBankAccountAsync(BankAccount model);
    }
}
