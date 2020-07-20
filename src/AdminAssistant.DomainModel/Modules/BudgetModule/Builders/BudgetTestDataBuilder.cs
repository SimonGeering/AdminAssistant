using System;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.TestDataBuilders
{
    public interface IBudgetTestDataBuilder
    {
        Budget Build();
        IBudgetTestDataBuilder WithTestData(int budgetID = Constants.UnknownRecordID);
        IBudgetTestDataBuilder WithBudgetName(string empty);
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
            this.BudgetName = "Test Budget";
            return this;
        }

        public IBudgetTestDataBuilder WithBudgetName(string budgetName)
        {
            this.BudgetName = budgetName;
            return this;
        }
    }
}
