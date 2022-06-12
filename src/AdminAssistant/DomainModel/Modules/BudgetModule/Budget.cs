namespace AdminAssistant.DomainModel.Modules.BudgetModule;

public record Budget : IDatabasePersistable
{
    public const int BudgetNameMaxLength = Constants.NameMaxLength;

    public int BudgetID { get; set; }
    public string BudgetName { get; set; } = string.Empty;

    public int PrimaryKey => BudgetID;
}
