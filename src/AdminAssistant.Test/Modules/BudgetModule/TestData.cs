using AdminAssistant.DomainModel.Modules.BudgetModule.TestDataBuilders;

namespace AdminAssistant.DomainModel.Modules.BudgetModule
{
    public static class TestData
    {
        public static IBudgetTestDataBuilder BudgetBuilder => new BudgetTestDataBuilder();
    }
}
