namespace AdminAssistant.Modules.CoreModule.UI;

public interface ICoreService
{
    Task<List<Currency>> GetCurrencyListAsync();
}

internal sealed class CoreService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, ILoggingProvider log)
    : ServiceBase(adminAssistantWebAPIClient, log), ICoreService
{
    public async Task<List<Currency>> GetCurrencyListAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetCurrencyAsync().ConfigureAwait(false);

        var result = new List<Currency>(response.ToCurrencyList());
        result.Insert(0, new Currency() { CurrencyID = CurrencyId.Default, Symbol = string.Empty, DecimalFormat = string.Empty });

        return Log.Finish(result);
    }
}
