namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders;

public interface IBankAccountTypeBuilder
{
    BankAccountType Build();
    IBankAccountTypeBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID);
    IBankAccountTypeBuilder WithDescription(string description);
}
