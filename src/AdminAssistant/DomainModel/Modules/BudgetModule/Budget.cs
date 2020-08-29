namespace AdminAssistant.DomainModel.Modules.BudgetModule
{
    public class Budget
    {
        public const int BudgetNameMaxLength = Constants.NameMaxLength;

        public string BudgetName { get; set; } = string.Empty;
    }
}
