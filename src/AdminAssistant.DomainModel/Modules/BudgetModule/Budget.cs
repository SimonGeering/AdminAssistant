using AdminAssistant.Shared;

namespace AdminAssistant.Modules.BudgetModule;

public sealed record Budget : IPersistable
{
    public const int BudgetNameMaxLength = EntityName.MaxLength;

    public BudgetId BudgetID { get; set; } = BudgetId.Default;
    public string BudgetName { get; set; } = string.Empty;

    public Id PrimaryKey => BudgetID;
}
public sealed record BudgetId(int Value) : Id(Value)
{
    public static BudgetId Default => new(Constants.UnknownRecordID);
}
