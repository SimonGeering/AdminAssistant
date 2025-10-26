using AdminAssistant.WebAPIClient.v1.CoreModule;

namespace AdminAssistant.Modules.CoreModule.UI;

public interface ICoreService
{
    Task<List<Currency>> GetCurrencyListAsync();
}

internal sealed class CoreService(ICurrencyApiClient currencyApiClient, ILoggingProvider log)
    : ServiceBase(log), ICoreService
{
    public async Task<List<Currency>> GetCurrencyListAsync()
    {
        Log.Start();

        var response = await currencyApiClient.GetCurrenciesAsync().ConfigureAwait(false);

        var result = new List<Currency>(response.ToCurrencyList());
        result.Insert(0, new Currency() { CurrencyID = CurrencyId.Default, Symbol = string.Empty, DecimalFormat = string.Empty });

        return Log.Finish(result);
    }
}
