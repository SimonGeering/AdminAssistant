using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.UI.Modules.Accounts
{
    public interface IAccountsService
    {
        Task<IList<BankAccountType>> LoadBankAccountTypesLookupDataAsync();
        Task<IList<Currency>> LoadCurrencyLookupDataAsync();
    }
}
