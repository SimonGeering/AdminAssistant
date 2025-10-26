using AdminAssistant.Modules.BudgetModule;

namespace AdminAssistant.WebAPI.v1.BudgetModule;

public static class BudgetModuleMapper
{
    public static IEnumerable<BudgetResponseDto> ToBudgetResponseDtoEnumeration(this IEnumerable<Budget> source)
        => source.Select(x => new BudgetResponseDto
        {
            BudgetID = x.BudgetID.Value,
            BudgetName = x.BudgetName
        });
}
