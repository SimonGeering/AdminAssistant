using AdminAssistant.WebAPI.v1.AccountsModule;

namespace AdminAssistant.WebAPIClient.v1.AccountsModule;

public interface IBankAccountInfoApiClient
{
    [Get("/api/v1/accounts-module/BankAccountInfo")]
    Task<IEnumerable<BankAccountInfoResponseDto>> GetBankAccountInfoAsync(CancellationToken cancellationToken = default);
}
