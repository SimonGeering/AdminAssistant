using AdminAssistant.WebAPI.v1.BudgetModule;

namespace AdminAssistant.WebAPIClient.v1.BudgetModule;

public interface IBudgetApiClient
{
    [Get("/api/v1/budget-module/Budget")]
    Task<IEnumerable<BudgetResponseDto>> GetBudgetsAsync(CancellationToken cancellationToken = default);
}
