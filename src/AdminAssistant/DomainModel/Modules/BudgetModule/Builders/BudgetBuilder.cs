namespace AdminAssistant.DomainModel.Modules.BudgetModule.Builders;

public interface IBudgetBuilder
{
    Budget Build();
    IBudgetBuilder WithTestData(int budgetID = Constants.UnknownRecordID);
    IBudgetBuilder WithBudgetName(string empty);
}
internal class BudgetBuilder : IBudgetBuilder
{
    private Budget _budget = new();

    public static Budget Default(IBudgetBuilder builder) => builder.Build();

    public Budget Build() => _budget;

    public IBudgetBuilder WithTestData(int budgetID = Constants.UnknownRecordID)
    {
        _budget = _budget with
        {
            BudgetID = budgetID,
            BudgetName = "Test Budget"
        };
        return this;
    }

    public IBudgetBuilder WithBudgetName(string budgetName)
    {
        _budget = _budget with { BudgetName = budgetName };
        return this;
    }
}
