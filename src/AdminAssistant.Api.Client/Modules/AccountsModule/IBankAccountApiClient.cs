using AdminAssistant.WebAPI.v1.AccountsModule;

namespace AdminAssistant.WebAPIClient.v1.AccountsModule;

public interface IBankAccountApiClient
{
    [Put("/api/v1/accounts-module/BankAccount")]
    Task<BankAccountResponseDto> UpdateBankAccountAsync([Body] BankAccountUpdateRequestDto request, CancellationToken cancellationToken = default);

    [Post("/api/v1/accounts-module/BankAccount")]
    Task<BankAccountResponseDto> CreateBankAccountAsync([Body] BankAccountCreateRequestDto request, CancellationToken cancellationToken = default);

    [Get("/api/v1/accounts-module/BankAccount/{bankAccountID}")]
    Task<BankAccountResponseDto> GetBankAccountByIdAsync(int bankAccountID, CancellationToken cancellationToken = default);

    [Get("/api/v1/accounts-module/BankAccount/{bankAccountID}/transactions")]
    Task<IEnumerable<BankAccountTransactionResponseDto>> GetBankAccountTransactionsAsync(int bankAccountID, CancellationToken cancellationToken = default);
}
