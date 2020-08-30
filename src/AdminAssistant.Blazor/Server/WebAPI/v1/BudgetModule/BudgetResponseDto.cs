using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1
{
    public class BudgetResponseDto : IMapFrom<Budget>
    {
        public int BudgetID { get; set; }
        public string BudgetName { get; set; } = string.Empty;
    }
}
