using AdminAssistant.WebAPI.v1.CoreModule;

namespace AdminAssistant.WebAPIClient.v1.CoreModule;

public interface ICurrencyApiClient
{
    [Put("/api/v1/core-module/Currency")]
    Task<CurrencyResponseDto> UpdateCurrencyAsync([Body] CurrencyUpdateRequestDto request, CancellationToken cancellationToken = default);

    [Post("/api/v1/core-module/Currency")]
    Task<CurrencyResponseDto> CreateCurrencyAsync([Body] CurrencyCreateRequestDto request, CancellationToken cancellationToken = default);

    [Get("/api/v1/core-module/Currency/{currencyID}")]
    Task<CurrencyResponseDto> GetCurrencyByIdAsync(int currencyID, CancellationToken cancellationToken = default);

    [Get("/api/v1/core-module/Currency")]
    Task<IEnumerable<CurrencyResponseDto>> GetCurrenciesAsync(CancellationToken cancellationToken = default);
}
