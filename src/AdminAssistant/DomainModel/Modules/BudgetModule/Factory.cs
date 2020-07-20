using AdminAssistant.DomainModel.Modules.BudgetModule.Builders;

namespace AdminAssistant.DomainModel.Modules.BudgetModule
{
    public static class Factory
    {
        public static IBudgetBuilder Budget => new BudgetBuilder();
    }
}
