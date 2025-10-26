using AdminAssistant.WebAPI.v1.AccountsModule;

namespace AdminAssistant.WebAPIClient.v1.AccountsModule;

public interface IBankAccountTypeApiClient
{
    [Get("/api/v1/accounts-module/BankAccountType")]
    Task<IEnumerable<BankAccountTypeResponseDto>> GetBankAccountTypesAsync(CancellationToken cancellationToken = default);
}
