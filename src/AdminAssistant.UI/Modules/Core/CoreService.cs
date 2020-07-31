using System.Collections.Generic;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using AdminAssistant.WebAPI.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.CoreModule
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CoreService : ServiceBase, ICoreService
    {
        public CoreService(IHttpClientProvider httpClientProvider, IMapper mapper, ILoggingProvider log)
            : base(httpClientProvider, mapper, log)
        {
        }
        
        public async Task<List<Currency>> GetCurrencyListAsync()
        {
            this.Log.Start();

            var response = await this.HttpClient.GetFromJsonAsync<CurrencyResponseDto[]>("api/v1/core/Currency").ConfigureAwait(false);

            var result = new List<Currency>(this.Mapper.Map<IEnumerable<Currency>>(response));
            result.Insert(0, new Currency() { CurrencyID = 0, Symbol = string.Empty, DecimalFormat = string.Empty });

            return this.Log.Finish(result);
        }
    }
}
