namespace AdminAssistant.DomainModel.Modules.BudgetModule.TestDataBuilders
{
    public interface IBudgetTestDataBuilder
    {
        Budget Build();
        IBudgetTestDataBuilder WithTestData(int budgetID = Constants.UnknownRecordID);
    }
    public class BudgetTestDataBuilder : Budget, IBudgetTestDataBuilder
    {
        public static Budget Default(IBudgetTestDataBuilder builder) => builder.Build();

        public Budget Build()
        {
            return this;
        }

        public IBudgetTestDataBuilder WithTestData(int budgetID = Constants.UnknownRecordID)
        {
            //this.I
            //this.Symbol = "GBP";

            return this;
        }
    }
}
