using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DomainModel;

namespace AdminAssistant.Accounts.DAL
{
    public interface ICurrencyRepository
    {
        Task<IList<Currency>> GetCurrencyListAsync();
    }
}
