namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountBuilder
{
    BankAccount Build();
    IBankAccountBuilder WithTestData(int bankAccountID = Constants.UnknownRecordID);
    IBankAccountBuilder WithBankAccountTypeID(int bankAccountTypeID);
    IBankAccountBuilder WithCurrencyID(int currency);
    IBankAccountBuilder WithAccountName(string accountName);
}
