using System.Diagnostics.CodeAnalysis;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;

namespace AdminAssistant.UI.Modules.CoreModule
{
    [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CoreService : ServiceBase, ICoreService
    {
        public CoreService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
            : base(adminAssistantWebAPIClient, mapper, log)
        {
        }
        
        public async Task<List<Currency>> GetCurrencyListAsync()
        {
            Log.Start();

            var response = await AdminAssistantWebAPIClient.GetCurrencyAsync().ConfigureAwait(false);

            var result = new List<Currency>(Mapper.Map<IEnumerable<Currency>>(response));
            result.Insert(0, new Currency() { CurrencyID = Constants.UnknownRecordID, Symbol = string.Empty, DecimalFormat = string.Empty });

            return Log.Finish(result);
        }
    }
}
