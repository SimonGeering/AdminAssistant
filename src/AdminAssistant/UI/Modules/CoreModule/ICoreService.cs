using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.UI.Modules.CoreModule
{
    public interface ICoreService
    {
        Task<List<Currency>> GetCurrencyListAsync();
    }
}
