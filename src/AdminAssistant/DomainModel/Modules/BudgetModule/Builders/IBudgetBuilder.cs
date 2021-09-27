namespace AdminAssistant.DomainModel.Modules.BudgetModule.Builders;

public interface IBudgetBuilder
{
    Budget Build();
    IBudgetBuilder WithTestData(int budgetID = Constants.UnknownRecordID);
    IBudgetBuilder WithBudgetName(string empty);
}
