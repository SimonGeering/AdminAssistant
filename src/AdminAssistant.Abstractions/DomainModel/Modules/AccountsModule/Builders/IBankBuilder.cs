namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankBuilder
{
    Bank Build();
    IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID);
    IBankBuilder WithBankName(string bankName);
}
