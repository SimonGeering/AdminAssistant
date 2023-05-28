namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IPayeeBuilder
{
    Payee Build();
    IPayeeBuilder WithTestData(int payeeID = Constants.UnknownRecordID);
    IPayeeBuilder WithName(string name);
}
