namespace AdminAssistant.DomainModel.Modules.BudgetModule.Builders
{
    public interface IBudgetBuilder
    {
        Budget Build();
        IBudgetBuilder WithTestData(int budgetID = Constants.UnknownRecordID);
        IBudgetBuilder WithBudgetName(string empty);
    }
    internal class BudgetBuilder : Budget, IBudgetBuilder
    {
        public static Budget Default(IBudgetBuilder builder) => builder.Build();

        public Budget Build() => this;

        public IBudgetBuilder WithTestData(int budgetID = Constants.UnknownRecordID)
        {
            BudgetName = "Test Budget";
            return this;
        }

        public IBudgetBuilder WithBudgetName(string budgetName)
        {
            BudgetName = budgetName;
            return this;
        }
    }
}
