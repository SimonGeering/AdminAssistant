using AdminAssistant.Modules.BudgetModule;

namespace AdminAssistant.WebAPI.v1.BudgetModule;

public sealed record BudgetResponseDto : IMapFrom<Budget>
{
    public int BudgetID { get; init; }
    public string BudgetName { get; init; } = string.Empty;
}
