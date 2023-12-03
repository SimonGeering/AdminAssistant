namespace AdminAssistant.Modules.CoreModule.UI;

public interface ICoreService
{
    Task<List<Currency>> GetCurrencyListAsync();
}

internal sealed class CoreService(IAdminAssistantWebAPIClient adminAssistantWebAPIClient, IMapper mapper, ILoggingProvider log)
    : ServiceBase(adminAssistantWebAPIClient, mapper, log), ICoreService
{
    public async Task<List<Currency>> GetCurrencyListAsync()
    {
        Log.Start();

        var response = await AdminAssistantWebAPIClient.GetCurrencyAsync().ConfigureAwait(false);

        var result = new List<Currency>(Mapper.Map<IEnumerable<Currency>>(response));
        result.Insert(0, new Currency() { CurrencyID = CurrencyId.Default, Symbol = string.Empty, DecimalFormat = string.Empty });

        return Log.Finish(result);
    }
}
