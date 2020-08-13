using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.CoreModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CoreService : ServiceBase, ICoreService
    {
        public CoreService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
            : base(adminAssistantWebAPIClient, mapper, log)
        {
        }
        
        public async Task<IList<Currency>> GetCurrencyListAsync()
        {
            this.Log.Start();

            var response = await this.AdminAssistantWebAPIClient.GetCurrencyAsync().ConfigureAwait(false);

            var result = new List<Currency>(this.Mapper.Map<IEnumerable<Currency>>(response));
            result.Insert(0, new Currency() { CurrencyID = 0, Symbol = string.Empty, DecimalFormat = string.Empty });

            return this.Log.Finish(result);
        }
    }
}
