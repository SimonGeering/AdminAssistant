namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountTypeId(int Value) : Id(Value)
{
    public static BankAccountTypeId Default => new(Constants.UnknownRecordID);
}
