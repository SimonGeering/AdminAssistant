using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.Accounts;

namespace AdminAssistant.DAL.Modules.Accounts
{
    public interface ICurrencyRepository
    {
        Task<IList<Currency>> GetCurrencyListAsync();
    }
}
