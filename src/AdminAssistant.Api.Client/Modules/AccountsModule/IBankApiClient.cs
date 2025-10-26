using AdminAssistant.WebAPI.v1.AccountsModule;

namespace AdminAssistant.WebAPIClient.v1.AccountsModule.AdminAssistant.WebAPIClient.v1.AccountsModule;

public interface IBankApiClient
{
    [Put("/api/v1/accounts-module/Bank")]
    Task<BankResponseDto> UpdateBankAsync([Body] BankUpdateRequestDto request, CancellationToken cancellationToken = default);

    [Post("/api/v1/accounts-module/Bank")]
    Task<BankResponseDto> CreateBankAsync([Body] BankCreateRequestDto request, CancellationToken cancellationToken = default);

    [Get("/api/v1/accounts-module/Bank/{bankID}")]
    Task<BankResponseDto> GetBankByIdAsync(int bankID, CancellationToken cancellationToken = default);

    [Get("/api/v1/accounts-module/Bank")]
    Task<IEnumerable<BankResponseDto>> GetBanksAsync(CancellationToken cancellationToken = default);
}
