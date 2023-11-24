namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountInfoBuilder
{
    BankAccountInfo Build();
    IBankAccountInfoBuilder WithTestData(int bankAccountInfoID = Constants.UnknownRecordID);
}
