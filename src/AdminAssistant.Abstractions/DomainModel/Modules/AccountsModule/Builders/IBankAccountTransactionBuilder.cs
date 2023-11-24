namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountTransactionBuilder
{
    BankAccountTransaction Build();
    IBankAccountTransactionBuilder WithTestData(int bankAccountTransactionID = Constants.UnknownRecordID);
    IBankAccountTransactionBuilder WithBankAccountID(int bankAccountID);
    IBankAccountTransactionBuilder WithDescription(string description);
}
